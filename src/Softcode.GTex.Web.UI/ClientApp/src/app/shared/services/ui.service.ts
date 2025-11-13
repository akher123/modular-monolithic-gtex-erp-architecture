import { Injectable } from '@angular/core';


@Injectable()
export class UiService {
  constructor() {

  }

  public show:boolean = true;
  public sidebarToggle: any = 'Show';

  //toggle() {
  //  this.show = !this.show;

  //  if (this.show)
  //    this.sidebarToggle = "Hide";
  //  else
  //    this.sidebarToggle = "Show";
  //}


  

}
