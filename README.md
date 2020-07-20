# TrackerPositionTransfer
Simple components to share trackers(or general gameobjects) via UDP in local network.

## QuickStart
- Import [MessagePack for C#](https://github.com/neuecc/MessagePack-CSharp/releases) on your unity first.
- Import this by .unitypackage from releases page.
- Attach sender/receiver component to any gameObject.
- Set tracker's Transform references on inspector.

## Notices
- Make sure that your project can use MessagePack for C#.
- Make sure that you can receive UDP (port is 3910). Check transport reguration settings of each OS. Or shut firewall down temporally.
