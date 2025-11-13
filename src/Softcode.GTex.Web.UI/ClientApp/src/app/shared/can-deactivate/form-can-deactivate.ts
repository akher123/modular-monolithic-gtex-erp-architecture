//import { ComponentCanDeactivate } from '../can-deactivate/component-can-deactivate';
import { NgForm } from "@angular/forms";
import { ComponentCanDeactivate } from "./component-can-deactivate";

export abstract class FormCanDeactivate extends ComponentCanDeactivate {

    abstract get userForm(): NgForm;
    
    canDeactivate(): boolean {
        console.log('FormCanDeactivate -  start');
        debugger;
        return this.userForm.submitted || !this.userForm.dirty
    }
}
