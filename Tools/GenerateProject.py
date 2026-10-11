#!/usr/bin/env python3
"""Generates Unity project files for JANTAR MANTAR RUN (Unity 2022.3 LTS).

Creates: ProjectSettings, .meta files, 2 scenes, 5 prefabs, sample materials.
Scenes are intentionally minimal: gameplay visuals are built at runtime by
the C# scripts (Greybox), so the YAML stays small and robust.

Usage: python3 Tools/GenerateProject.py
"""
import os, uuid, hashlib

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
YAML_HEAD = "%YAML 1.1\n%TAG !u! tag:unity3d.com,2011:\n"

def guid_for(path):
    return uuid.uuid5(uuid.NAMESPACE_URL, "jantarmantar/" + path).hex

def write_meta(asset_path, importer_block):
    meta = "fileFormatVersion: 2\nguid: %s\n%s" % (guid_for(asset_path), importer_block)
    with open(os.path.join(ROOT, asset_path + ".meta"), "w") as f:
        f.write(meta)

FOLDER_META = "folderAsset: yes\nDefaultImporter:\n  externalObjects: {}\n  userData: \n  assetBundleName: \n  assetBundleVariant: \n"
SCRIPT_META = ("MonoImporter:\n  externalObjects: {}\n  serializedVersion: 2\n"
               "  defaultReferences: []\n  executionOrder: 0\n  icon: {instanceID: 0}\n"
               "  userData: \n  assetBundleName: \n  assetBundleVariant: \n")
SCENE_META = ("SceneAsset:\n  externalObjects: {}\n  userData: \n"
              "  assetBundleName: \n  assetBundleVariant: \n")
PREFAB_META = ("PrefabImporter:\n  externalObjects: {}\n  userData: \n"
               "  assetBundleName: \n  assetBundleVariant: \n")
MAT_META = ("NativeFormatImporter:\n  externalObjects: {}\n  mainObjectFileID: 2100000\n"
            "  userData: \n  assetBundleName: \n  assetBundleVariant: \n")

# ---------------------------------------------------------------- yaml blocks
class ID:
    def __init__(self): self.n = 0
    def nxt(self, base):
        self.n += 1
        return base + self.n

def gameobject(ids, name, components, active=True, tag="Untagged", layer=0):
    gid = ids.nxt(10000000)
    comp_lines = "\n".join("    - component: {fileID: %d}" % c for c in components)
    return gid, (
        "--- !u!1 &%d\nGameObject:\n  m_ObjectHideFlags: 0\n"
        "  m_CorrespondingSourceObject: {fileID: 0}\n  m_PrefabInstance: {fileID: 0}\n"
        "  m_PrefabAsset: {fileID: 0}\n  serializedVersion: 6\n  m_Component:\n%s\n"
        "  m_Layer: %d\n  m_Name: %s\n  m_TagString: %s\n  m_Icon: {fileID: 0}\n"
        "  m_NavMeshLayer: 0\n  m_StaticEditorFlags: 0\n  m_IsActive: %d\n"
        % (gid, comp_lines, layer, name, tag, 1 if active else 0))

def transform(ids, go_id, pos=(0, 0, 0), rot=(0, 0, 0), scale=(1, 1, 1), parent=0, children=()):
    tid = ids.nxt(40000000)
    child_lines = "".join("    - {fileID: %d}\n" % c for c in children)
    return tid, (
        "--- !u!4 &%d\nTransform:\n  m_ObjectHideFlags: 0\n"
        "  m_CorrespondingSourceObject: {fileID: 0}\n  m_PrefabInstance: {fileID: 0}\n"
        "  m_PrefabAsset: {fileID: 0}\n  m_GameObject: {fileID: %d}\n"
        "  m_LocalRotation: {x: 0, y: 0, z: 0, w: 1}\n"
        "  m_LocalPosition: {x: %s, y: %s, z: %s}\n"
        "  m_LocalScale: {x: %s, y: %s, z: %s}\n"
        "  m_ConstrainProportionsScale: 0\n  m_Children:\n%s  m_Father: {fileID: %d}\n"
        "  m_RootOrder: 0\n  m_LocalEulerAnglesHint: {x: %s, y: %s, z: %s}\n"
        % (tid, go_id, pos[0], pos[1], pos[2], scale[0], scale[1], scale[2],
           child_lines, parent, rot[0], rot[1], rot[2]))

def mono(ids, go_id, script_path, fields=""):
    mid = ids.nxt(114000000)
    guid = guid_for(script_path)
    extra = ("\n" + fields) if fields else ""
    return mid, (
        "--- !u!114 &%d\nMonoBehaviour:\n  m_ObjectHideFlags: 0\n"
        "  m_CorrespondingSourceObject: {fileID: 0}\n  m_PrefabInstance: {fileID: 0}\n"
        "  m_PrefabAsset: {fileID: 0}\n  m_GameObject: {fileID: %d}\n  m_Enabled: 1\n"
        "  m_EditorHideFlags: 0\n  m_Script: {fileID: 11500000, guid: %s, type: 3}\n"
        "  m_Name: \n  m_EditorClassIdentifier: %s" % (mid, go_id, guid, extra))

def camera(ids, go_id):
    cid = ids.nxt(20000000)
    return cid, (
        "--- !u!20 &%d\nCamera:\n  m_ObjectHideFlags: 0\n"
        "  m_CorrespondingSourceObject: {fileID: 0}\n  m_PrefabInstance: {fileID: 0}\n"
        "  m_PrefabAsset: {fileID: 0}\n  m_GameObject: {fileID: %d}\n  m_Enabled: 1\n"
        "  serializedVersion: 3\n  m_ClearFlags: 1\n"
        "  m_BackGroundColor: {r: 0.53, g: 0.75, b: 1, a: 1}\n"
        "  m_projectionMatrixMode: 1\n  m_SensorSize: {x: 36, y: 24}\n  m_LensShift: {x: 0, y: 0}\n"
        "  m_FocalLength: 50\n  m_NormalizedViewPortRect: {x: 0, y: 0, width: 1, height: 1}\n"
        "  near clip plane: 0.3\n  far clip plane: 1000\n  field of view: 60\n  orthographic: 0\n"
        "  orthographic size: 5\n  m_Depth: -1\n  m_CullingMask:\n    serializedVersion: 2\n    m_Bits: 4294967295\n"
        "  m_RenderingPath: -1\n  m_TargetTexture: {fileID: 0}\n  m_TargetDisplay: 0\n"
        "  m_TargetEye: 3\n  m_HDR: 1\n  m_AllowMSAA: 1\n  m_AllowDynamicResolution: 0\n"
        "  m_ForceIntoRT: 0\n  m_OcclusionCulling: 1\n  m_StereoConvergence: 10\n  m_StereoSeparation: 0.022\n"
        % (cid, go_id))

def light(ids, go_id):
    lid = ids.nxt(108000000)
    return lid, (
        "--- !u!108 &%d\nLight:\n  m_ObjectHideFlags: 0\n"
        "  m_CorrespondingSourceObject: {fileID: 0}\n  m_PrefabInstance: {fileID: 0}\n"
        "  m_PrefabAsset: {fileID: 0}\n  m_GameObject: {fileID: %d}\n  m_Enabled: 1\n"
        "  serializedVersion: 10\n  m_Type: 1\n  m_Shape: 0\n"
        "  m_Color: {r: 1, g: 0.96, b: 0.88, a: 1}\n  m_Intensity: 1.1\n"
        "  m_Range: 10\n  m_SpotAngle: 30\n  m_InnerSpotAngle: 21.8\n  m_CookieSize: 10\n"
        "  m_Shadows:\n    m_Type: 2\n    m_Resolution: -1\n    m_CustomResolution: -1\n    m_Strength: 1\n"
        "    m_Bias: 0.05\n    m_NormalBias: 0.4\n    m_NearPlane: 0.2\n  m_Cookie: {fileID: 0}\n"
        "  m_DrawHalo: 0\n  m_Flare: {fileID: 0}\n  m_RenderMode: 0\n  m_CullingMask:\n"
        "    serializedVersion: 2\n    m_Bits: 4294967295\n  m_RenderingLayerMask: 1\n"
        "  m_Lightmapping: 4\n  m_LightShadowCasterMode: 0\n  m_AreaSize: {x: 1, y: 1}\n"
        "  m_BounceIntensity: 1\n  m_ColorTemperature: 6570\n  m_UseColorTemperature: 0\n"
        % (lid, go_id))

def audio_listener(ids, go_id):
    aid = ids.nxt(81000000)
    return aid, (
        "--- !u!81 &%d\nAudioListener:\n  m_ObjectHideFlags: 0\n"
        "  m_CorrespondingSourceObject: {fileID: 0}\n  m_PrefabInstance: {fileID: 0}\n"
        "  m_PrefabAsset: {fileID: 0}\n  m_GameObject: {fileID: %d}\n  m_Enabled: 1\n"
        % (aid, go_id))

def audio_source(ids, go_id):
    aid = ids.nxt(82000000)
    return aid, (
        "--- !u!82 &%d\nAudioSource:\n  m_ObjectHideFlags: 0\n"
        "  m_CorrespondingSourceObject: {fileID: 0}\n  m_PrefabInstance: {fileID: 0}\n"
        "  m_PrefabAsset: {fileID: 0}\n  m_GameObject: {fileID: %d}\n  m_Enabled: 1\n"
        "  serializedVersion: 4\n  Output: {fileID: 0}\n  m_AudioClip: {fileID: 0}\n"
        "  m_PlayOnAwake: 0\n  m_Volume: 1\n  m_Pitch: 1\n"
        % (aid, go_id))

def rigidbody(ids, go_id, kinematic=True):
    rid = ids.nxt(54000000)
    return rid, (
        "--- !u!54 &%d\nRigidbody:\n  m_ObjectHideFlags: 0\n"
        "  m_CorrespondingSourceObject: {fileID: 0}\n  m_PrefabInstance: {fileID: 0}\n"
        "  m_PrefabAsset: {fileID: 0}\n  m_GameObject: {fileID: %d}\n"
        "  serializedVersion: 2\n  m_Mass: 1\n  m_Drag: 0\n  m_AngularDrag: 0.05\n"
        "  m_UseGravity: 0\n  m_IsKinematic: %d\n  m_Interpolate: 0\n  m_Constraints: 0\n"
        "  m_CollisionDetection: 0\n"
        % (rid, go_id, 1 if kinematic else 0))

def box_collider(ids, go_id, center=(0, 1, 0), size=(1, 2, 1), trigger=False):
    cid = ids.nxt(65000000)
    return cid, (
        "--- !u!65 &%d\nBoxCollider:\n  m_ObjectHideFlags: 0\n"
        "  m_CorrespondingSourceObject: {fileID: 0}\n  m_PrefabInstance: {fileID: 0}\n"
        "  m_PrefabAsset: {fileID: 0}\n  m_GameObject: {fileID: %d}\n"
        "  m_Material: {fileID: 0}\n  m_IsTrigger: %d\n  m_Enabled: 1\n"
        "  serializedVersion: 3\n  m_Size: {x: %s, y: %s, z: %s}\n"
        "  m_Center: {x: %s, y: %s, z: %s}\n"
        % (cid, go_id, 1 if trigger else 0, size[0], size[1], size[2],
           center[0], center[1], center[2]))

def sphere_collider(ids, go_id, radius=0.7, trigger=True):
    cid = ids.nxt(135000000)
    return cid, (
        "--- !u!135 &%d\nSphereCollider:\n  m_ObjectHideFlags: 0\n"
        "  m_CorrespondingSourceObject: {fileID: 0}\n  m_PrefabInstance: {fileID: 0}\n"
        "  m_PrefabAsset: {fileID: 0}\n  m_GameObject: {fileID: %d}\n"
        "  m_Material: {fileID: 0}\n  m_IsTrigger: %d\n  m_Enabled: 1\n"
        "  serializedVersion: 3\n  m_Radius: %s\n  m_Center: {x: 0, y: 0, z: 0}\n"
        % (cid, go_id, 1 if trigger else 0, radius))

def prefab_ref(prefab_path, root_file_id):
    return "{fileID: %d, guid: %s, type: 3}" % (root_file_id, guid_for(prefab_path))

# ---------------------------------------------------------------- scenes
def build_opening_scene():
    ids, parts = ID(), []
    def go(name, comps, **kw):
        gid, y = gameobject(ids, name, comps, **kw)
        parts.append(y)
        return gid
    def tr(go_id, **kw):
        tid, y = transform(ids, go_id, **kw)
        parts.append(y)
        return tid

    # Main Camera
    gid = go("Main Camera", [0, 0, 0])
    tid = tr(gid, pos=(0, 3, -8), rot=(10, 0, 0))
    cid, cy = camera(ids, gid); parts.append(cy)
    aid, ay = audio_listener(ids, gid); parts.append(ay)
    parts[0] = parts[0].replace("    - component: {fileID: 0}\n" * 3,
        "    - component: {fileID: %d}\n    - component: {fileID: %d}\n    - component: {fileID: %d}\n" % (tid, cid, aid), 1)

    # Directional Light
    gid = go("Directional Light", [0])
    tid = tr(gid, pos=(0, 3, 0), rot=(50, -30, 0))
    lid, ly = light(ids, gid); parts.append(ly)
    parts[-3] = parts[-3].replace("    - component: {fileID: 0}\n    - component: {fileID: 0}",
        "    - component: {fileID: %d}\n    - component: {fileID: %d}" % (tid, lid), 1)

    # OpeningSequence
    gid = go("OpeningSequence", [0, 0])
    tid = tr(gid)
    mid, my = mono(ids, gid, "Assets/Scripts/OpeningSequence.cs"); parts.append(my)
    parts[-3] = parts[-3].replace("    - component: {fileID: 0}\n    - component: {fileID: 0}",
        "    - component: {fileID: %d}\n    - component: {fileID: %d}" % (tid, mid), 1)

    # GameManager
    gid = go("GameManager", [0, 0])
    tid = tr(gid)
    mid, my = mono(ids, gid, "Assets/Scripts/GameManager.cs"); parts.append(my)
    parts[-3] = parts[-3].replace("    - component: {fileID: 0}\n    - component: {fileID: 0}",
        "    - component: {fileID: %d}\n    - component: {fileID: %d}" % (tid, mid), 1)

    # AudioManager
    gid = go("AudioManager", [0, 0, 0])
    tid = tr(gid)
    mid, my = mono(ids, gid, "Assets/Scripts/AudioManager.cs"); parts.append(my)
    aid, ay = audio_source(ids, gid); parts.append(ay)
    parts[-4] = parts[-4].replace("    - component: {fileID: 0}\n" * 3,
        "    - component: {fileID: %d}\n    - component: {fileID: %d}\n    - component: {fileID: %d}\n" % (tid, mid, aid), 1)

    return YAML_HEAD + "".join(parts)

def build_game_scene(prefab_ids):
    ids, parts = ID(), []
    def go(name, comps, **kw):
        gid, y = gameobject(ids, name, comps, **kw)
        parts.append(y)
        return gid
    def tr(go_id, **kw):
        tid, y = transform(ids, go_id, **kw)
        parts.append(y)
        return tid
    def wire(go_idx, *cids):
        # replace placeholder component slots in the go block at parts[go_idx]
        repl = "".join("    - component: {fileID: %d}\n" % c for c in cids)
        parts[go_idx] = parts[go_idx].replace("    - component: {fileID: 0}\n" * len(cids), repl, 1)

    # Main Camera + follow
    g = go("Main Camera", [0, 0, 0, 0]); gi = len(parts) - 1
    t = tr(g, pos=(0, 4.6, -7.6))
    c, cy = camera(ids, g); parts.append(cy)
    a, ay = audio_listener(ids, g); parts.append(ay)
    m, my = mono(ids, g, "Assets/Scripts/CameraFollow.cs"); parts.append(my)
    wire(gi, t, c, a, m)

    # Directional Light
    g = go("Directional Light", [0, 0]); gi = len(parts) - 1
    t = tr(g, pos=(0, 3, 0), rot=(50, -30, 0))
    l, ly = light(ids, g); parts.append(ly)
    wire(gi, t, l)

    # Player
    g = go("Player", [0, 0, 0, 0]); gi = len(parts) - 1
    t = tr(g, pos=(0, 0, 0))
    r, ry = rigidbody(ids, g, kinematic=True); parts.append(ry)
    bc, by = box_collider(ids, g, center=(0, 1, 0), size=(1, 2, 1)); parts.append(by)
    m, my = mono(ids, g, "Assets/Scripts/PlayerController.cs"); parts.append(my)
    wire(gi, t, r, bc, m)

    # Spawners & systems
    def sys(name, script, fields=""):
        g = go(name, [0, 0]); gi = len(parts) - 1
        t = tr(g)
        m, my = mono(ids, g, script, fields); parts.append(my)
        wire(gi, t, m)
        return g

    sys("RoadSpawner", "Assets/Scripts/RoadSpawner.cs")
    sys("ObstacleSpawner", "Assets/Scripts/ObstacleSpawner.cs",
        "  barricadePrefab: %s\n  carPrefab: %s\n  overheadPrefab: %s" % (
            prefab_ref("Assets/Prefabs/Barricade.prefab", prefab_ids["Barricade"]),
            prefab_ref("Assets/Prefabs/Car.prefab", prefab_ids["Car"]),
            prefab_ref("Assets/Prefabs/Overhead.prefab", prefab_ids["Overhead"])))
    sys("CoinSpawner", "Assets/Scripts/CoinSpawner.cs",
        "  coinPrefab: %s" % prefab_ref("Assets/Prefabs/Coin.prefab", prefab_ids["Coin"]))
    sys("PoliceChase", "Assets/Scripts/PoliceChase.cs")
    sys("GameManager", "Assets/Scripts/GameManager.cs")
    sys("UIManager", "Assets/Scripts/UIManager.cs")
    sys("TouchInput", "Assets/Scripts/TouchInput.cs")
    sys("DetentionSequence", "Assets/Scripts/DetentionSequence.cs")

    # AudioManager
    g = go("AudioManager", [0, 0, 0]); gi = len(parts) - 1
    t = tr(g)
    m, my = mono(ids, g, "Assets/Scripts/AudioManager.cs"); parts.append(my)
    a, ay = audio_source(ids, g); parts.append(ay)
    wire(gi, t, m, a)

    return YAML_HEAD + "".join(parts)

# ---------------------------------------------------------------- prefabs
def build_prefab(name, script_path, extra_components, root_file_id=10000001):
    """Minimal prefab: root GO + transform + script + colliders. Visuals built at runtime."""
    ids, parts = ID(), []
    ids.n = 0
    gid = root_file_id
    comp_ids = []
    tid = 40000001
    parts.append(
        "--- !u!1 &%d\nGameObject:\n  m_ObjectHideFlags: 0\n"
        "  m_CorrespondingSourceObject: {fileID: 0}\n  m_PrefabInstance: {fileID: 0}\n"
        "  m_PrefabAsset: {fileID: 0}\n  serializedVersion: 6\n  m_Component:\n__COMPS__"
        "  m_Layer: 0\n  m_Name: %s\n  m_TagString: Untagged\n  m_Icon: {fileID: 0}\n"
        "  m_NavMeshLayer: 0\n  m_StaticEditorFlags: 0\n  m_IsActive: 1\n"
        % (gid, name))
    parts.append(
        "--- !u!4 &%d\nTransform:\n  m_ObjectHideFlags: 0\n"
        "  m_CorrespondingSourceObject: {fileID: 0}\n  m_PrefabInstance: {fileID: 0}\n"
        "  m_PrefabAsset: {fileID: 0}\n  m_GameObject: {fileID: %d}\n"
        "  m_LocalRotation: {x: 0, y: 0, z: 0, w: 1}\n"
        "  m_LocalPosition: {x: 0, y: 0, z: 0}\n"
        "  m_LocalScale: {x: 1, y: 1, z: 1}\n"
        "  m_ConstrainProportionsScale: 0\n  m_Children: []\n  m_Father: {fileID: 0}\n"
        "  m_RootOrder: 0\n  m_LocalEulerAnglesHint: {x: 0, y: 0, z: 0}\n" % (tid, gid))
    comp_ids.append(tid)
    # script
    mid = 114000001
    guid = guid_for(script_path)
    kind_field = ""
    if name in ("Barricade", "Car", "Overhead"):
        kind_field = "\n  kind: %d" % {"Barricade": 0, "Car": 1, "Overhead": 2}[name]
    parts.append(
        "--- !u!114 &%d\nMonoBehaviour:\n  m_ObjectHideFlags: 0\n"
        "  m_CorrespondingSourceObject: {fileID: 0}\n  m_PrefabInstance: {fileID: 0}\n"
        "  m_PrefabAsset: {fileID: 0}\n  m_GameObject: {fileID: %d}\n  m_Enabled: 1\n"
        "  m_EditorHideFlags: 0\n  m_Script: {fileID: 11500000, guid: %s, type: 3}\n"
        "  m_Name: \n  m_EditorClassIdentifier: %s" % (mid, gid, guid, kind_field))
    comp_ids.append(mid)
    nxt = 65000001
    for kind, params in extra_components:
        if kind == "box":
            c, s = params.get("center", (0, 1, 0)), params.get("size", (1, 2, 1))
            tg = params.get("trigger", False)
            parts.append(
                "--- !u!65 &%d\nBoxCollider:\n  m_ObjectHideFlags: 0\n"
                "  m_CorrespondingSourceObject: {fileID: 0}\n  m_PrefabInstance: {fileID: 0}\n"
                "  m_PrefabAsset: {fileID: 0}\n  m_GameObject: {fileID: %d}\n"
                "  m_Material: {fileID: 0}\n  m_IsTrigger: %d\n  m_Enabled: 1\n"
                "  serializedVersion: 3\n  m_Size: {x: %s, y: %s, z: %s}\n"
                "  m_Center: {x: %s, y: %s, z: %s}\n"
                % (nxt, gid, 1 if tg else 0, s[0], s[1], s[2], c[0], c[1], c[2]))
            comp_ids.append(nxt); nxt += 1
        elif kind == "sphere":
            parts.append(
                "--- !u!135 &%d\nSphereCollider:\n  m_ObjectHideFlags: 0\n"
                "  m_CorrespondingSourceObject: {fileID: 0}\n  m_PrefabInstance: {fileID: 0}\n"
                "  m_PrefabAsset: {fileID: 0}\n  m_GameObject: {fileID: %d}\n"
                "  m_Material: {fileID: 0}\n  m_IsTrigger: 1\n  m_Enabled: 1\n"
                "  serializedVersion: 3\n  m_Radius: %s\n  m_Center: {x: 0, y: 0, z: 0}\n"
                % (nxt, gid, params.get("radius", 0.7)))
            comp_ids.append(nxt); nxt += 1
        elif kind == "rigidbody":
            parts.append(
                "--- !u!54 &%d\nRigidbody:\n  m_ObjectHideFlags: 0\n"
                "  m_CorrespondingSourceObject: {fileID: 0}\n  m_PrefabInstance: {fileID: 0}\n"
                "  m_PrefabAsset: {fileID: 0}\n  m_GameObject: {fileID: %d}\n"
                "  serializedVersion: 2\n  m_Mass: 1\n  m_Drag: 0\n  m_AngularDrag: 0.05\n"
                "  m_UseGravity: 0\n  m_IsKinematic: 1\n  m_Interpolate: 0\n  m_Constraints: 0\n"
                "  m_CollisionDetection: 0\n" % (nxt, gid))
            comp_ids.append(nxt); nxt += 1
    comp_lines = "".join("    - component: {fileID: %d}\n" % c for c in comp_ids)
    parts[0] = parts[0].replace("__COMPS__", comp_lines)
    return YAML_HEAD + "".join(parts), gid

# ---------------------------------------------------------------- main
def main():
    os.makedirs(os.path.join(ROOT, "Assets/Scenes"), exist_ok=True)
    os.makedirs(os.path.join(ROOT, "Assets/Prefabs"), exist_ok=True)
    os.makedirs(os.path.join(ROOT, "Assets/Materials"), exist_ok=True)
    os.makedirs(os.path.join(ROOT, "Assets/Environment"), exist_ok=True)
    os.makedirs(os.path.join(ROOT, "Assets/Characters"), exist_ok=True)
    os.makedirs(os.path.join(ROOT, "Assets/Audio"), exist_ok=True)
    os.makedirs(os.path.join(ROOT, "Assets/UI"), exist_ok=True)
    os.makedirs(os.path.join(ROOT, "ProjectSettings"), exist_ok=True)
    for d in ["Assets", "Assets/Scenes", "Assets/Scripts", "Assets/Prefabs",
              "Assets/Materials", "Assets/Environment", "Assets/Characters",
              "Assets/Audio", "Assets/UI", "Tools", "Tools/Verify",
              "WebPrototype", "Documentation"]:
        p = os.path.join(ROOT, d)
        if os.path.isdir(p):
            write_meta(d, FOLDER_META)

    # script metas
    for f in os.listdir(os.path.join(ROOT, "Assets/Scripts")):
        if f.endswith(".cs"):
            write_meta("Assets/Scripts/" + f, SCRIPT_META)

    # prefabs
    prefab_ids = {}
    defs = [
        ("Player", "Assets/Scripts/PlayerController.cs",
         [("rigidbody", {}), ("box", {"center": (0, 1, 0), "size": (1, 2, 1), "trigger": False})]),
        ("Barricade", "Assets/Scripts/Obstacle.cs",
         [("box", {"center": (0, 0.6, 0), "size": (2.0, 1.2, 0.6), "trigger": True})]),
        ("Car", "Assets/Scripts/Obstacle.cs",
         [("box", {"center": (0, 0.9, 0), "size": (1.9, 1.8, 3.6), "trigger": True})]),
        ("Overhead", "Assets/Scripts/Obstacle.cs",
         [("box", {"center": (0, 1.6, 0), "size": (2.35, 1.1, 0.5), "trigger": True})]),
        ("Coin", "Assets/Scripts/Coin.cs",
         [("sphere", {"radius": 0.7})]),
    ]
    for name, script, comps in defs:
        yml, gid = build_prefab(name, script, comps)
        path = "Assets/Prefabs/%s.prefab" % name
        with open(os.path.join(ROOT, path), "w") as f:
            f.write(yml)
        write_meta(path, PREFAB_META)
        prefab_ids[name] = gid

    # scenes
    for sname, builder in [("Opening", lambda: build_opening_scene()),
                           ("Game", lambda: build_game_scene(prefab_ids))]:
        path = "Assets/Scenes/%s.unity" % sname
        with open(os.path.join(ROOT, path), "w") as f:
            f.write(builder())
        write_meta(path, SCENE_META)

    # sample materials
    for mname, color in [("Asphalt", (0.16, 0.16, 0.18, 1)), ("Sandstone", (0.85, 0.72, 0.55, 1))]:
        yml = (YAML_HEAD + "--- !u!21 &2100000\nMaterial:\n  serializedVersion: 8\n"
               "  m_ObjectHideFlags: 0\n  m_CorrespondingSourceObject: {fileID: 0}\n"
               "  m_PrefabInstance: {fileID: 0}\n  m_PrefabAsset: {fileID: 0}\n  m_Name: %s\n"
               "  m_Shader: {fileID: 4800000, guid: 0000000000000000f000000000000000, type: 0}\n"
               "  m_ShaderKeywords: \n  m_LightmapFlags: 4\n  m_EnableInstancingVariants: 0\n"
               "  m_DoubleSidedGI: 0\n  m_CustomRenderQueue: -1\n"
               "  stringTagMap: {}\n  disabledShaderPasses: []\n"
               "  m_SavedProperties:\n    serializedVersion: 3\n    m_TexEnvs: []\n"
               "    m_Ints: []\n    m_Floats: []\n"
               "    m_Colors:\n    - _Color: {r: %s, g: %s, b: %s, a: %s}\n" % (
                   mname, color[0], color[1], color[2], color[3]))
        path = "Assets/Materials/%s.mat" % mname
        with open(os.path.join(ROOT, path), "w") as f:
            f.write(yml)
        write_meta(path, MAT_META)

    # ProjectVersion
    with open(os.path.join(ROOT, "ProjectSettings/ProjectVersion.txt"), "w") as f:
        f.write("m_EditorVersion: 2022.3.22f1\nm_EditorVersionWithRevision: 2022.3.22f1 (b7fc0279e33d)\n")

    # EditorBuildSettings (scene list)
    ebs = (YAML_HEAD + "--- !u!104 &1\nEditorBuildSettings:\n  m_ObjectHideFlags: 0\n"
           "  serializedVersion: 2\n  m_Scenes:\n"
           "  - enabled: 1\n    path: Assets/Scenes/Opening.unity\n    guid: %s\n"
           "  - enabled: 1\n    path: Assets/Scenes/Game.unity\n    guid: %s\n"
           "  m_configObjects: {}\n" % (guid_for("Assets/Scenes/Opening.unity"),
                                       guid_for("Assets/Scenes/Game.unity")))
    with open(os.path.join(ROOT, "ProjectSettings/EditorBuildSettings.asset"), "w") as f:
        f.write(ebs)

    # minimal ProjectSettings
    ps = (YAML_HEAD + "--- !u!129 &1\nPlayerSettings:\n  m_ObjectHideFlags: 0\n"
          "  serializedVersion: 23\n  m_CompanyName: JantarMantar\n"
          "  m_ProductName: Jantar Mantar Run - Delhi Chase\n"
          "  m_ApplicationIdentifier:\n    Android: com.jantarmantar.delhichase\n"
          "    Standalone: com.jantarmantar.delhichase\n"
          "  m_BuildTargetIcons: []\n  defaultScreenOrientation: 3\n"
          "  m_ScreenOrientation: 0\n"
          "--- !u!104 &2\nEditorSettings:\n  m_ObjectHideFlags: 0\n"
          "  serializedVersion: 3\n  m_ExternalVersionControlSupport: 1\n"
          "  m_SerializationMode: 2\n")
    with open(os.path.join(ROOT, "ProjectSettings/ProjectSettings.asset"), "w") as f:
        f.write(ps)

    print("generated OK")

if __name__ == "__main__":
    main()
