# Rail Sim Remote

...Is a REST-style web server that provides access to the
[RailDriver](http://raildriver.com/products/raildriver.php) API provided with
Dovetail Games' Train Simulator (formerly "RailWorks") games. This API allows
external programs to not only manipulate the train's controls, but also read
status information such as the current speed and position of the train. This
information powers apps like
[Train Maps Live](https://web.archive.org/web/20180704124447/http://haywardstudios.co.uk/train-maps-live/).

Rail Sim Remote represents the latest in a
[very,](https://github.com/cheesestraws/rdip)
[very,](https://github.com/alios/raildriver)
[very](https://www.trainsim.com/vbts/showthread.php?325000-TSConductor-Train-Simulator-TCP-Interface)
[long](https://github.com/piotrkilczuk/py-raildriver)
[line](https://github.com/reallyinsane/trainsimulator-controller)
of previous similar projects. So, why create yet another one?

* HTTP/JSON-style API that any web browser can speak
* Design is transparent enough to interoperate with every locomotive
* Support for writing to, not just reading controls
* Support for the "virtual controls" (speed, position, heading, etc.)
* (Exclusive) support for the 64-bit version of Train Simulator
* Turnkey web server for easy web app hosting

### Usage

The server serves files out of `.\wwwroot` and by default listens on
`http://localhost:8888`. You can change the latter with the `--urls <URL(s)>`
parameter.