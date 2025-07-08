import xml.etree.ElementTree as ET

csproj = "MyNotepad.csproj"
tree = ET.parse(csproj)
root = tree.getroot()
ns = {'msbuild': root.tag.split('}')[0].strip('{')} if '}' in root.tag else {}
property_group = root.find('msbuild:PropertyGroup', ns) if ns else root.find('PropertyGroup')
version_node = None
if property_group is not None:
    version_node = property_group.find('msbuild:Version', ns) if ns else property_group.find('Version')
if property_group is None:
    property_group = ET.SubElement(root, 'PropertyGroup')
if version_node is None:
    version_node = ET.SubElement(property_group, 'Version')
    version_node.text = '1.0.0'
version = version_node.text or '1.0.0'
parts = version.split('.')
while len(parts) < 3:
    parts.append('0')
parts[1] = str(int(parts[1]) + 1)
parts[2] = '0'
new_version = f"{parts[0]}.{parts[1]}.{parts[2]}"
version_node.text = new_version
print(f"Version updated to {new_version}")
tree.write(csproj, encoding='utf-8', xml_declaration=True)
