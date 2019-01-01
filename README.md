# PathFinder

// new (branch:two)
wpf project: demonstrates different line/ animation options for connecting two nodes.

todo: 
adjust target circle to match target node
add a viewmodel

![alt text](https://github.com/dtaylor-530/PathFinder/blob/two/010119Snapshot.png "Snapshot of program")

// original (branch:master)
Wpf project that displays two nodes and the path that connects them via
an orthogonal line (i.e one that is composed of at least two segments at a right angle).

The orthogonal line is calculated using vector maths whose explanation is included in the code.

The MVVM approach is lightely used.

This could be utlised in a flowchart-diagram based project.


![alt text](https://github.com/dtaylor-530/PathFinder/blob/two/OriginalSnapShot.png "Snapshot of program")


Issues include:

fixing the position of the connection tips to the node's center or a position on the node's boundary relative to it.

limiting the movement of nodes to the dimensions of the canvas.
