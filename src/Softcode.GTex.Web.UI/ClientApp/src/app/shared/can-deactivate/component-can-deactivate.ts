import { HostListener } from "@angular/core";

export abstract class ComponentCanDeactivate {

    abstract canDeactivate(): boolean;

    @HostListener('window:beforeunload', ['$event'])
    unloadNotification($event: any) {       
        console.log('ComponentCanDeactivate -  start');
        if (!this.canDeactivate()) {
            $event.returnValue = true;
        }
    }
}
