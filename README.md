
# CBM (Cross-platform Broadcast Messenger)

This is a set of three applications allowing to send simple text messages between multiple platforms utilising UDP broadcasting.

Supported platforms are 
* iOS 14 and above
* macOS 10.15 and above (yes, it's Big Sur ready)
* Windows 10 (uwp)

The text messages will be broadcasted to any other device running the app in the local network. But no direct connection will be made.
Non of the messages are stored anywhere; so an app has to be up and running to receive a message.

The apps are written in C\# only, using UWP for windows and Xamarin (not forms) for iOS and macOS apps. A .netStandard library contains all the functionality shared across the platforms.
 
I made these apps out of necessity, as I need to be able to send some text to other devices while using multiple computers. I know there are full fledged solutions, but I did not want to subscribe yet to another service and found it to be a neat little project.

I hope other developers can use the code as an example for UDP data broadcasting using C\#.

Note:
iOS does not allow sending local udp data without a special privilege. Hence it is just a receiver.
