import { Scene, Environment, OrbitControls, SkyDome } from 'https://esm.sh/@viggle/splat-engine@0.1.1?bundle&target=es2022';
import JSZip from 'https://esm.sh/jszip@3.10.1?bundle';
// Load the road-only archive first; the previous all-in-one archive was ~75 MB.
const ARCHIVE='https://d2ol7oe51mr4n9.cloudfront.net/user_3JkuoLy4uHNDjSxNT7XRC1ogkuo/9ff44b0b-7dc8-4c8c-9774-578f3fc19f2e.zip';
const canvas=document.querySelector('#canvas'),loading=document.querySelector('#loading'),status=document.querySelector('#loadstatus');
const titles={road:['Road and footpath','Asphalt, lane markings, crossing and sidewalks.'],trees:['Trees and green verge','Tree environment will be added after the road preview.'],buildings:['Sansad Marg buildings','Building environment will be added after the road preview.'],observatory:['Jantar Mantar observatory','Observatory environment will be added after the road preview.']};
let scene,current=null,urls=[];
async function main(){
 status.textContent='Downloading road preview (smaller asset bundle)…';
 const res=await fetch(ARCHIVE);if(!res.ok)throw Error('Road bundle request failed: '+res.status);
 const zip=await JSZip.loadAsync(await res.arrayBuffer());
 const files=Object.keys(zip.files).filter(n=>n.endsWith('.spz'));
 if(!files.length)throw Error('Road asset is missing from the bundle.');
 const road=await zip.file(files.find(n=>n.toLowerCase().includes('road'))||files[0]).async('arraybuffer');
 status.textContent='Starting 3D renderer…';
 scene=await Scene.create(canvas,{fov:45,depth:true});scene.start();
 const controls=new OrbitControls({distance:8,pitch:12,target:{x:0,y:1,z:0}});controls.attach({canvas,cameraEntity:scene.cameraEntity});scene.app.on('update',()=>controls.update());SkyDome.create(scene,{topColor:'#b7d7e7',bottomColor:'#f5e7cd'});
 const url=URL.createObjectURL(new Blob([road],{type:'application/octet-stream'}));urls.push(url);
 document.querySelectorAll('[data-scene]').forEach(b=>b.addEventListener('click',()=>choose(b.dataset.scene,url)));
 await choose('road',url);
}
async function choose(k,url){
 document.querySelectorAll('[data-scene]').forEach(b=>b.classList.toggle('active',b.dataset.scene===k));
 document.querySelector('#sceneTitle').textContent=titles[k][0];document.querySelector('#sceneDesc').textContent=titles[k][1];
 if(k!=='road'){status.textContent='This section is not included in the lightweight first-load build yet. Road preview is available.';return}
 loading.classList.remove('hidden');loading.querySelector('h1').textContent='Loading road and footpath…';status.textContent='Loading road environment…';
 try{if(current){current.destroy?.();current=null}current=await Environment.load(scene,url);loading.classList.add('hidden')}
 catch(e){loading.querySelector('h1').textContent='Road scene could not load';status.textContent=String(e.message||e);console.error(e)}
}
main().catch(e=>{loading.querySelector('h1').textContent='PINOC world could not start';status.textContent=String(e.message||e);console.error(e)});addEventListener('beforeunload',()=>urls.forEach(URL.revokeObjectURL));