import { Component, OnInit } from "@angular/core";
import { NgxUiLoaderService } from "ngx-ui-loader";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
})
export class AppComponent implements OnInit {
  public dashboardColor: boolean = true;

  constructor(private ngxLoader: NgxUiLoaderService) {}
  ngOnInit() {
    const body = document.getElementsByTagName("body")[0];
    if (this.dashboardColor === false) {
      this.changeDashboardColor("");
    } else {
      this.changeDashboardColor("white-content");
    }
    //load spin
    this.ngxLoader.startLoader("loader-01"); // start non-master loader
    setTimeout(() => {
      this.ngxLoader.stopLoader("loader-01");
    }, 300);
    //end load spin

    
    // we simulate the window Resize so the charts will get updated in realtime.
    var simulateWindowResize = setInterval(function () {
      window.dispatchEvent(new Event("resize"));
    }, 180);

    // we stop the simulation of Window Resize after the animations are completed
    setTimeout(function () {
      clearInterval(simulateWindowResize);
    }, 2000);
  }
  changeDashboardColor(color) {
    var body = document.getElementsByTagName("body")[0];
    if (body && color === "white-content") {
      body.classList.add(color);
    } else if (body.classList.contains("white-content")) {
      body.classList.remove("white-content");
    }
  }
}
