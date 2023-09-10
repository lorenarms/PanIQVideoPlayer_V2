<a name="readme-top"></a>



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]


[product-screenshot]: https://github.com/lorenarms/PanIQVideoPlayer_V2/blob/master/PanIQVideoPlayer_V2/Images/main%20pages%20(Medium).png

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://paniqescaperoom.com">
    <img src="https://github.com/lorenarms/PanIQVideoPlayer_V2/blob/master/PanIQVideoPlayer_V2/Images/home-logo%20(Custom).png" alt="Logo">
  </a>

  <h3 align="center">Messenger and Video Player</h3>

  <p align="center">
    A Windows Form application written in C#
    <br />
    <a href="https://github.com/lorenarms/PanIQVideoPlayer_V2"><strong>Explore the files »</strong></a>
    <br />
    <br />
    <a href="https://youtu.be/UMUskVbR8X4">Messenger App Demo</a>
    ·
    <a href="https://youtu.be/Q7Q1f1BY-rs">Video Player Demo</a>
    ·
    <a href="https://www.youtube.com/watch?v=ltE63Xm3bh4&list=PLhz6FAyiBzY6kAOeiksSwaB5887EGQIyY">See more projects</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#screenshots">Screenshots</a>
    </li>
    <li><a href="#more-information">More Information</a></li>
    <li><a href="#setup">Setup</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

For this project I was commissioned by my company, PanIQ Room, to design and build an easy-to-use application that would allow for a user to play a video on a remote computer. 

[![Product Name Screen Shot][product-screenshot]](http:artllj.com/about)

PanIQ Room is an escape room company, and the goal was to cut down on the time it takes to introduce players to the rules of the room they were about to play. An introduction video was written and filmed (also by me), to be shown before each game started. To streamline the process of showing the video, management wanted the Game Masters (located in the Control room) to be able to remotely set the video to play. 

### The Goal

When a host leaves the team of players in the room, the game master in the control room will be able to run this application to set the introductory video to play on the video screen of the in-room kiosk. When the video is finished, the screen will automatically switch to the in-room application that allows players to communicate with the game master. 

Below is a diagram illustrating this process:

<img src="https://github.com/lorenarms/PanIQVideoPlayer_V2/blob/master/PanIQVideoPlayer_V2/Images/Diagram.jpg" style="width:500px">

In order to accomplish this task I first developed a messenger application to familiarize myself with the TCP protocol. The app allows one computer to act as a server while another hosts a client application that can send and receive messages to and from the server. To learn how to do this I followed <a href="https://www.youtube.com/watch?v=QrdfegS3iDg">this tutorial</a>. Below is a screenshot of the completed messaging application, talking to itself on my computer.

<table>
  <tr>
    <td>Server Messenger
    <td>Client Messenger
  </tr>
  <tr>
    <td><img src="https://github.com/lorenarms/PanIQVideoPlayer_V2/blob/master/PanIQVideoPlayer_V2/Images/server%20original.png" alt="Server" style="max-width:400px;width: expression(this.width > 400 ? 400: true);">
    <td><img src="https://github.com/lorenarms/PanIQVideoPlayer_V2/blob/master/PanIQVideoPlayer_V2/Images/client%20original.png" alt="Client" style="max-width:400px;width: expression(this.width > 400 ? 400: true);">
  </tr>
</table>

Here is the Messenger Application in action, running both parts on my computer:
<img src="https://github.com/lorenarms/PanIQVideoPlayer_V2/blob/master/PanIQVideoPlayer_V2/Images/MessengerAppDemo.gif" style="width:1000px">

### The Process

Once I better understood how the process worked, I set out to modify the application to fit our business needs. The application needed to:

- [ ] Be extremely user friendly
- [ ] Allow multiple 'servers' to control any number of 'kiosks'
- [ ] Work quickly and seemlessly so as not to interrupt player experience


In order to complete this task, I developed an application that contains three parts: a 'server', a 'master', and a 'client'. The 'master' is actually just another 'client' application that is skinned to look and behave differently from other clients. The 'server' facilitates communication between all parts of the system, routing messages from the source to their requested destination. This setup allows for multiple 'master' applications to control multiple 'client' applications at once. Since we have multiple control-room computers, this was an essential part of development.

The diagram below illustrates the end result. Two different control-room computers can both simultaneously control three (or more) different kiosk computers that are on the same network. The server between them all manages the traffic.

<img src="https://github.com/lorenarms/PanIQVideoPlayer_V2/blob/master/PanIQVideoPlayer_V2/Images/Diagram%202_2.jpg" style="width:500px">

The server, master, and client all have methods in their programming that decode a specialized header-string system so that messages do not get crossed.

<img src="https://github.com/lorenarms/PanIQVideoPlayer_V2/blob/master/PanIQVideoPlayer_V2/Images/PanIQVideoDemo.gif" style="width:1000px">

Make sure you view the <a href="https://youtu.be/ZRTjin782os">demo</a> to get more information about how all this works.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Built With

This project was built with the following applications, languages, and libraries. Additionally, the project makes use of <a href="https://bootboxjs.com/">Bootbox.js</a> for custom dialogs, and <a href="https://datatables.net/">Datatables</a> for easy-to-use database display.

[![VS][Visual Studio]][vs-url]
[![csharp][csharp]][csharp-url]

[Visual Studio]: https://img.shields.io/badge/visual_studio_2022-ffffff?style=for-the-badge&logo=visualstudio&logoColor=purple
[vs-url]: https://visualstudio.microsoft.com/vs/
[dotnet]: https://img.shields.io/badge/Microsoft_.net-ffffff?style=for-the-badge&logo=dotnet&logoColor=purple
[net-url]: https://visualstudio.microsoft.com/vs/](https://dotnet.microsoft.com/en-us/
[csharp]: https://img.shields.io/badge/C_Sharp-590ec4?style=for-the-badge&logo=csharp&logoColor=white
[csharp-url]: https://dotnet.microsoft.com/en-us/languages/csharp

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Screenshots

<table>
<tr>
    <td>Server</td>
  </tr>
  <tr>
    <td><img src="https://github.com/lorenarms/PanIQVideoPlayer_V2/blob/master/PanIQVideoPlayer_V2/Images/server.png" style="width:500px">
  </tr>
  <tr>
    <td>Control Room Application</td>
  </tr>
  <tr>
    <td><img src="https://github.com/lorenarms/PanIQVideoPlayer_V2/blob/master/PanIQVideoPlayer_V2/Images/master.png" style="width:500px">
  </tr>
  
</table>

<!-- GETTING STARTED -->
## Getting Started

Fork the project, or copy the repo and get going!

### Prerequisites

You'll want to start with <a href="https://visualstudio.microsoft.com/">Visual Studio</a>

### Installation

_You'll need 'SuperSimpleTCP' for C#. You can find it in the NuGet package manager_

I will provide a video in the near future showing how to do this.

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- OTHER ITEMS -->
## More Information

*    The application is designed to be dropped onto any computers that are on the same network and then just "work". It needed to be easy to run for users who don't really know how to use computers or troublshoot computer issues. 

*    The server application needs to be running on one computer, but beyond that any number of kiosk applications and control-panel applications can be run on any number of computers. The server will handle all communication.

*    I have not tested this with large-scale networks, so don't quote me on the "any number" thing.

*    The secret of the server-client communication is that, depending on which button is pressed on the form, a different "header" is added to the "message" string that is routed to the desired client. Watch the information video to see this in action.

*    The "client list" in the control-panel application depends on the names of the client computers, so they all need to be unique so there's no confusion. Actual communication relies on ip addresses, so no messages get delivered to the wrong address.

<p align="right">(<a href="#readme-top">back to top</a>)</p>


## Setup

Running the "server" application will display the ip address that all other apps need to connect to. The default port is 9001.

<img src="https://github.com/lorenarms/PanIQVideoPlayer_V2/blob/master/PanIQVideoPlayer_V2/Images/server%20ip.png" style="width:500px">

The Kiosk application allows the user to change which ip address to connect to, so be sure to change this to the address displayed on the server.

<img src="https://github.com/lorenarms/PanIQVideoPlayer_V2/blob/master/PanIQVideoPlayer_V2/Images/client%20ip.png" style="width:500px">

The control-panel application's ip address is populated automatically (so that users cannot inadvertantly change it on accident). Devs should change the "Connection" string value in the "ClientMasterForm" code to match the ip address displayed by the server.

<img src="https://github.com/lorenarms/PanIQVideoPlayer_V2/blob/master/PanIQVideoPlayer_V2/Images/master%20ip.png" style="width:500px">

The CommandRunner class in the Client.Slave application contains all the commands to run the video on the client computer. The method StartIntroVideo is called when the "PLAY" header is received; modify these commands to 
- [X] start other programs
- [X] play other videos
- [X] do something else

<img src="https://github.com/lorenarms/PanIQVideoPlayer_V2/blob/master/PanIQVideoPlayer_V2/Images/commandrunnerclass.png" style="width:500px">

Additionally, you may add other methods to this class to do different things. Be sure to add 'headers' to discern between these different commands.

<!-- ROADMAP -->
## Roadmap

- [ ] Change UI of Kiosk app
- [ ] Test for scale
- [ ] Add 'admin-mode' to control-panel app

See the [open issues](https://github.com/lorenarms/PanIQVideoPlayer_V2/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- LICENSE -->
## License

Currently there is no license for this application, it is free to use by anyone who needs it. Please consider letting me know if it was helpful at all, or any additions you can propose (see the <a href="#contributing">contributing</a> section)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

<p>Check out my <a href="https://www.youtube.com/channel/UCGtp8PRHgPCQHYoSxbMST8A" target="_blank">YouTube channel</a> for more videos about coding projects I've done.</p>
<p>Also, check out my <a href="http://artllj.com" target="_blank">Personal Website</a> for more information about me, and my <a href="https://www.linkedin.com/in/lorenarms95/" target="_blank">LinkedIn</a> to see if I'd be a good fit for your team. </p>
<h3>Thanks for stopping by!</h3>
<img src="https://github.com/lorenarms/SNHU_CS_370_Emerging_Trends_in_CS/blob/main/images/profile.png" alt="[picture of me]" style="width:100px;">
<p>much love
-L
</p>

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* [Best README Template](https://github.com/lorenarms/README-Template)

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/lorenarms/PanIQVideoPlayer_V2.svg?style=for-the-badge
[contributors-url]: https://github.com/lorenarms/PanIQVideoPlayer_V2/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/lorenarms/PanIQVideoPlayer_V2.svg?style=for-the-badge
[forks-url]: https://github.com/lorenarms/PanIQVideoPlayer_V2/forks
[stars-shield]: https://img.shields.io/github/stars/lorenarms/PanIQVideoPlayer_V2.svg?style=for-the-badge
[stars-url]: https://github.com/lorenarms/PanIQVideoPlayer_V2/stargazers
[issues-shield]: https://img.shields.io/github/issues/lorenarms/PanIQVideoPlayer_V2.svg?style=for-the-badge
[issues-url]: https://github.com/lorenarms/PanIQVideoPlayer_V2/issues
[license-shield]: https://img.shields.io/github/license/lorenarms/vidly.svg?style=for-the-badge
[license-url]: https://github.com/lorenarms/vidly/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&color=blue
[linkedin-url]: https://linkedin.com/in/lorenarms95





[Next.js]: https://img.shields.io/badge/next.js-000000?style=for-the-badge&logo=nextdotjs&logoColor=white
[Next-url]: https://nextjs.org/
[React.js]: https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB
[React-url]: https://reactjs.org/
[Vue.js]: https://img.shields.io/badge/Vue.js-35495E?style=for-the-badge&logo=vuedotjs&logoColor=4FC08D
[Vue-url]: https://vuejs.org/
[Angular.io]: https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white
[Angular-url]: https://angular.io/
[Svelte.dev]: https://img.shields.io/badge/Svelte-4A4A55?style=for-the-badge&logo=svelte&logoColor=FF3E00
[Svelte-url]: https://svelte.dev/
[Laravel.com]: https://img.shields.io/badge/Laravel-FF2D20?style=for-the-badge&logo=laravel&logoColor=white
[Laravel-url]: https://laravel.com
[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[Bootstrap-url]: https://getbootstrap.com
[JQuery.com]: https://img.shields.io/badge/jQuery-0769AD?style=for-the-badge&logo=jquery&logoColor=white
[JQuery-url]: https://jquery.com 
