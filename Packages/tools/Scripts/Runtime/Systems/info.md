## _Systems_
In poetools, systems are generic MonoBehaviours 
that **_can be used_** to drive some aspect of an object.
On their own, systems don't do anything: they only provide
a convenient interface for anyone that has a reference to it.

The complement to systems are the various Controllers: e.g.
FreecamController, FPSController. These MonoBehaviours reference
various systems, and feed them the data they need to work.

### Justification
The benefit of separating System code from Controller code
is almost entirely an architectural one - it follows the idea
of Single Responsibility. However, it also has practical
applications when it comes to code re-use: ideally, systems are 
able to be re-used for contexts outside the player. For example,
using RotationSystem to drive enemy rotations, benefiting from 
the rotation clamping it provides.