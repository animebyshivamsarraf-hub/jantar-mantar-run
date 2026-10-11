import bpy, math
from mathutils import Vector

# JANTAR MANTAR RUN — Delhi environment blockout
# Procedural, editable Blender 5.2 scene. All measurements are metres.
scene = bpy.context.scene
if scene.world is None:
    scene.world = bpy.data.worlds.new("World")
scene.world.color = (0.16, 0.18, 0.22)
scene.render.engine = "BLENDER_EEVEE"
scene.render.resolution_x, scene.render.resolution_y = 1000, 650
scene.render.resolution_percentage = 70

def material(name, color, roughness=0.8, metallic=0.0):
    m = bpy.data.materials.get(name) or bpy.data.materials.new(name)
    m.diffuse_color = (*color, 1)
    m.use_nodes = True
    bsdf = m.node_tree.nodes.get("Principled BSDF")
    if bsdf:
        bsdf.inputs["Base Color"].default_value = (*color, 1)
        bsdf.inputs["Roughness"].default_value = roughness
        bsdf.inputs["Metallic"].default_value = metallic
    return m

asphalt = material("MAT_Asphalt", (0.105, 0.13, 0.15))
concrete = material("MAT_Warm_Concrete", (0.48, 0.45, 0.39))
sandstone = material("MAT_Sandstone", (0.68, 0.31, 0.17))
terracotta = material("MAT_Terracotta_Trim", (0.84, 0.49, 0.27))
marking = material("MAT_Road_Marking", (0.88, 0.82, 0.63))
metal = material("MAT_Barricade_Metal", (0.28, 0.34, 0.37), 0.42, 0.55)
foliage = material("MAT_Tree_Foliage", (0.13, 0.29, 0.17))
wood = material("MAT_Tree_Trunk", (0.25, 0.16, 0.09))
buildings = material("MAT_Background_Buildings", (0.48, 0.43, 0.36))
poster = material("MAT_Placeholder_Poster", (0.86, 0.82, 0.69))

def box(name, loc, dims, mat, bevel=0):
    bpy.ops.mesh.primitive_cube_add(size=1, location=loc)
    obj = bpy.context.object
    obj.name = name
    obj.dimensions = dims
    bpy.ops.object.transform_apply(location=False, rotation=False, scale=True)
    if mat:
        obj.data.materials.append(mat)
    if bevel:
        mod = obj.modifiers.new("Soft_Edges", "BEVEL")
        mod.width, mod.segments = bevel, 2
        obj.modifiers.new("Weighted_Corners", "WEIGHTED_NORMAL")
    return obj

def cylinder(name, loc, radius, depth, mat, vertices=16):
    bpy.ops.mesh.primitive_cylinder_add(vertices=vertices, radius=radius, depth=depth, location=loc)
    obj = bpy.context.object
    obj.name = name
    if mat:
        obj.data.materials.append(mat)
    return obj

def aim(obj, point):
    obj.rotation_euler = (Vector(point) - obj.location).to_track_quat("-Z", "Y").to_euler()

# Base plate, plaza, road, sidewalks and road markings
box("ENV_Ground_Plate", (0, 8, -0.55), (64, 150, 1), concrete, 0.25)
box("ENV_Sansad_Marg_Road", (0, 24, 0.02), (14, 91, 0.12), asphalt, 0.05)
box("ENV_Left_Sidewalk", (-8.6, 24, 0.08), (3.1, 91, 0.22), concrete, 0.05)
box("ENV_Right_Sidewalk", (8.6, 24, 0.08), (3.1, 91, 0.22), concrete, 0.05)
box("ENV_Protest_Plaza", (0, -17, 0.06), (40, 30, 0.2), concrete, 0.12)
for i, y in enumerate(range(-17, 68, 8)):
    box("ROAD_Center_Dash_%02d" % i, (0, y, 0.105), (0.16, 3.8, 0.025), marking)
for x in (-6.55, 6.55):
    box("ROAD_Edge_Line_" + str(x), (x, 24, 0.105), (0.12, 90, 0.025), marking)

# Stylised Jantar Mantar-inspired sandstone massing, not a surveyed reconstruction
for side, label in [(-1, "L"), (1, "R")]:
    x = side * 15.5
    box("JANTAR_" + label + "_Plinth", (x, -15, 0.65), (7.5, 17, 1.2), sandstone, 0.18)
    box("JANTAR_" + label + "_Upper_Platform", (x, -15, 1.35), (5.8, 13, 0.28), terracotta, 0.08)
    for j in range(3):
        xx = x + side * (j * 0.7 - 0.7)
        box("JANTAR_" + label + "_Instrument_Block_" + str(j),
            (xx, -13 + j * 3, 2.1 + j * 0.35), (1.3, 3.2, 2.0 + j * 0.7), sandstone, 0.08)
    cylinder("JANTAR_" + label + "_Marker_Tower", (x, -22, 2.2), 0.65, 3.4, terracotta, 12)
    cylinder("JANTAR_" + label + "_Tower_Cap", (x, -22, 4.0), 0.85, 0.35, sandstone, 12)

for side in (-1, 1):
    box("PLAZA_Perimeter_Wall_" + str(side), (side * 21, -16, 0.65), (0.7, 25, 1.2), sandstone, 0.08)
for x in (-18, -12, 12, 18):
    box("PLAZA_Entry_Pier_" + str(x), (x, -1.2, 1.0), (0.8, 0.8, 2.0), terracotta, 0.07)

# Barricade line with a wide central opening for the player route
for idx, x in enumerate([-6.0, -4.5, -3.0, 3.0, 4.5, 6.0]):
    box("BARRICADE_%02d_Foot" % idx, (x, 5.5, 0.32), (0.65, 0.65, 0.18), metal, 0.04)
    box("BARRICADE_%02d_Rail" % idx, (x, 5.5, 0.92), (0.12, 0.12, 1.15), metal, 0.03)
    box("BARRICADE_%02d_Top" % idx, (x, 5.5, 1.45), (1.35, 0.14, 0.12), metal, 0.04)

# Background urban massing; keep road corridor readable
blocks = [
    (-24, 20, 7, 16, 12), (24, 22, 8, 18, 15),
    (-24, 44, 7, 18, 17), (24, 48, 8, 20, 13),
    (-19, 65, 11, 12, 15), (20, 68, 12, 14, 18),
    (-29, -2, 5, 16, 8), (29, -3, 5, 16, 9)
]
for i, (x, y, w, d, h) in enumerate(blocks):
    box("CITY_Block_%02d" % i, (x, y, h / 2), (w, d, h), buildings, 0.12)
    for level in range(1, max(2, int(h // 3))):
        box("CITY_Block_%02d_Band_%02d" % (i, level),
            (x, y, h / 2 + level * 2.7), (w + 0.04, d + 0.04, 0.12), terracotta)

# Simple tree proxies for scale and edge rhythm
for i, (x, y) in enumerate([(-11,-24),(11,-24),(-11,-10),(11,-10),(-11,13),(11,17),(-11,35),(11,39),(-11,56),(11,60)]):
    cylinder("TREE_%02d_Trunk" % i, (x, y, 1.0), 0.22, 1.9, wood, 10)
    bpy.ops.mesh.primitive_ico_sphere_add(subdivisions=1, radius=1, location=(x, y, 2.5))
    crown = bpy.context.object
    crown.name = "TREE_%02d_Crown" % i
    crown.scale = (1.05, 1.0, 1.25)
    crown.data.materials.append(foliage)

# Blank placard proxies only; user slogans/memes/audio are not inserted yet
for i, x in enumerate([-7, -4, 4, 7]):
    box("PROTEST_PLAQUE_%02d_Board" % i, (x, -18, 2.0), (1.0, 0.12, 0.85), poster, 0.04)
    box("PROTEST_PLAQUE_%02d_Handle" % i, (x, -18, 1.35), (0.08, 0.08, 1.15), wood)

# Delivery camera and working light rig
bpy.ops.object.camera_add(location=(43, -52, 39))
camera = bpy.context.object
camera.name = "CAM_main"
camera.data.lens = 43
aim(camera, (0, 18, 0.4))
scene.camera = camera
sun = bpy.data.objects.get("Web_sun")
if sun and sun.type == "LIGHT":
    sun.name = "LGT_key"
    sun.data.type = "SUN"
    sun.data.energy = 2.0
    aim(sun, (0, 0, 0))
else:
    bpy.ops.object.light_add(type="SUN", location=(0, -15, 30))
    sun = bpy.context.object
    sun.name = "LGT_key"
    sun.data.energy = 2.0
    aim(sun, (0, 0, 0))
bpy.ops.object.light_add(type="POINT", location=(5, -25, 13))
fill = bpy.context.object
fill.name = "LGT_fill"
fill.data.energy = 1800
fill.data.shadow_soft_size = 8
scene.frame_start, scene.frame_end = 1, 250
