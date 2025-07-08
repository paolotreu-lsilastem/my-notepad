import subprocess
import sys
import xml.etree.ElementTree as ET
import os

csproj = "MyNotepad.csproj"
publish_path = os.path.join("publish", "MyNotepad.exe")

def get_version():
    tree = ET.parse(csproj)
    root = tree.getroot()
    ns = {'msbuild': root.tag.split('}')[0].strip('{')} if '}' in root.tag else {}
    property_group = root.find('msbuild:PropertyGroup', ns) if ns else root.find('PropertyGroup')
    version_node = None
    if property_group is not None:
        version_node = property_group.find('msbuild:Version', ns) if ns else property_group.find('Version')
    if version_node is not None and version_node.text:
        return version_node.text.strip()
    return "0.0.0"

def main():
    version = get_version()
    tag = f"v{version}"
    title = f"MyNotepad v{version}"
    notes = f"Release v{version}"
    # Check if release already exists
    result = subprocess.run(["gh", "release", "view", tag], capture_output=True, text=True)
    if result.returncode == 0:
        print(f"ERROR: Release {tag} already exists.", file=sys.stderr)
        sys.exit(1)
    # Create release
    cmd = [
        "gh", "release", "create", tag,
        publish_path,
        "--title", title,
        "--notes", notes
    ]
    print(f"Running: {' '.join(cmd)}")
    subprocess.check_call(cmd)

if __name__ == "__main__":
    main()
