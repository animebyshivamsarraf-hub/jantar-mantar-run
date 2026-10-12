import { Scene, Environment, OrbitControls, SkyDome } from 'https://esm.sh/@viggle/splat-engine@0.1.1?bundle&target=es2022';
import JSZip from 'https://esm.sh/jszip@3.10.1?bundle';
const ARCHIVE='https://d2ol7oe51mr4n9.cloudfront.net/user_3JkuoLy4uHNDjSxNT7XRC1ogkuo/4fe31d28-dbde-49a5-a0c6-f471e586a4b5.zip';
const canvas=document.querySelector('#canvas'),loading=document.querySelector('#loading'),status=document.querySelector('#loadstatus');
const titles={road:['Road and footpath','Asphalt, lane markings, crossing and sidewalks.'],trees:['Trees and green verge','Roadside trees, planted areas and pedestrian paths.'],buildings:['Sansad Marg buildings','Delhi government-district facades, compound walls and gates.'],observatory:['Jantar Mantar observatory','Historic astronomical instruments and courtyard environment.']};
let scene,current=null,urls=[];
async function main(){
 status.textContent='Fetching PINOC asset bundle…';const res=await fetch(ARCHIVE);if(!res.ok)throw Error('Asset bundle request failed: '+res.status);
 const zip=await JSZip.loadAsync(await res.arrayBuffer()),files={};for(const k of Object.keys(titles)){const f=zip.file('pinoc-assets/'+k+'.spz');if(!f)throw Error('Missing '+k);files[k]=await f.async('arraybuffer')}
 status.textContent='Starting 3D Gaussian-splat renderer…';scene=await Scene.create(canvas,{fov:45,depth:true});scene.start();
 const controls=new OrbitControls({distance:8,pitch:12,target:{x:0,y:1,z:0}});controls.attach({canvas,cameraEntity:scene.cameraEntity});scene.app.on('update',()=>controls.update());SkyDome.create(scene,{topColor:'#b7d7e7',bottomColor:'#f5e7cd'});
 for(const k of Object.keys(files)){files[k]=URL.createObjectURL(new Blob([files[k]],{type:'application/octet-stream'}));urls.push(files[k])}
 document.querySelectorAll('[data-scene]').forEach(b=>b.addEventListener('click',()=>choose(b.dataset.scene,files)));
 await choose('road',files);
}
async function choose(k,files){document.querySelectorAll('[data-scene]').forEach(b=>b.classList.toggle('active',b.dataset.scene===k));document.querySelector('#sceneTitle').textContent=titles[k][0];document.querySelector('#sceneDesc').textContent=titles[k][1];loading.classList.remove('hidden');loading.querySelector('h1').textContent='Loading '+titles[k][0]+'…';status.textContent='Loading selected environment…';try{if(current){current.destroy?.();current=null}current=await Environment.load(scene,files[k]);loading.classList.add('hidden')}catch(e){loading.querySelector('h1').textContent='Scene could not load';status.textContent=String(e.message||e);console.error(e)}}
main().catch(e=>{loading.querySelector('h1').textContent='PINOC world could not start';status.textContent=String(e.message||e);console.error(e)});addEventListener('beforeunload',()=>urls.forEach(URL.revokeObjectURL));