import { Scene, Environment, OrbitControls, SkyDome } from 'https://esm.sh/@viggle/splat-engine@0.1.1?bundle&target=es2022';
import JSZip from 'https://esm.sh/jszip@3.10.1?bundle';
const ARCHIVE='https://d2ol7oe51mr4n9.cloudfront.net/user_3JkuoLy4uHNDjSxNT7XRC1ogkuo/9ff44b0b-7dc8-4c8c-9774-578f3fc19f2e.zip';
const canvas=document.querySelector('#canvas'),loading=document.querySelector('#loading'),status=document.querySelector('#loadstatus');
const titles={road:['Road and footpath','Asphalt, lane markings, crossing and sidewalks.'],trees:['Trees and green verge','This lightweight build loads the road first.'],buildings:['Sansad Marg buildings','This lightweight build loads the road first.'],observatory:['Jantar Mantar observatory','This lightweight build loads the road first.']};
let scene,current=null;
async function main(){
 status.textContent='Downloading road preview…';
 const res=await fetch(ARCHIVE);if(!res.ok)throw Error('Road bundle request failed: '+res.status);
 const zip=await JSZip.loadAsync(await res.arrayBuffer());
 const file=Object.keys(zip.files).find(n=>n.toLowerCase().endsWith('.spz'));
 if(!file)throw Error('Road SPZ asset is missing from the bundle.');
 const road=await zip.file(file).async('arraybuffer');
 status.textContent='Starting 3D renderer…';
 scene=await Scene.create(canvas,{fov:45,depth:true});scene.start();
 const controls=new OrbitControls({distance:8,pitch:12,target:{x:0,y:1,z:0}});controls.attach({canvas,cameraEntity:scene.cameraEntity});scene.app.on('update',()=>controls.update());SkyDome.create(scene,{topColor:'#b7d7e7',bottomColor:'#f5e7cd'});
 document.querySelectorAll('[data-scene]').forEach(b=>b.addEventListener('click',()=>choose(b.dataset.scene,road)));
 await choose('road',road);
}
async function choose(k,bytes){
 document.querySelectorAll('[data-scene]').forEach(b=>b.classList.toggle('active',b.dataset.scene===k));
 document.querySelector('#sceneTitle').textContent=titles[k][0];document.querySelector('#sceneDesc').textContent=titles[k][1];
 if(k!=='road'){status.textContent='Road preview is available first; other environments are not yet wired into this lightweight build.';return}
 loading.classList.remove('hidden');loading.querySelector('h1').textContent='Loading road and footpath…';status.textContent='Loading SPZ road environment…';
 try{if(current){current.destroy?.();current=null}current=await Environment.load(scene,bytes,{filename:'roadside.spz',name:'Road and Footpath'});loading.classList.add('hidden')}
 catch(e){loading.querySelector('h1').textContent='Road scene could not load';status.textContent=String(e.message||e);console.error(e)}
}
main().catch(e=>{loading.querySelector('h1').textContent='PINOC world could not start';status.textContent=String(e.message||e);console.error(e)});
