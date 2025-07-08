import xml.etree.ElementTree as ET

csproj = "MyNotepad.csproj"
tree = ET.parse(csproj)
root = tree.getroot()

# Handle namespace
ns = {'msbuild': root.tag.split('}')[0].strip('{')} if '}' in root.tag else {}
property_group = root.find('msbuild:PropertyGroup', ns) if ns else root.find('PropertyGroup')
version_node = None
if property_group is not None:
    version_node = property_group.find('msbuild:Version', ns) if ns else property_group.find('Version')

if version_node is not None and version_node.text:
    print(version_node.text.strip())
else:
    print("0.0.0")
