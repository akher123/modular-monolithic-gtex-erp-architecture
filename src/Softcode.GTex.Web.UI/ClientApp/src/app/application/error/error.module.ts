import { NgModule } from '@angular/core';
import { RoutedComponents, ErrorRoutingModule } from './error-routing.module';
import { ErrorComponent } from './error.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@NgModule({
    imports: [
        ErrorRoutingModule
        , FormsModule
        , CommonModule
    ],
    exports: [
        
    ],
    declarations: [
        ...RoutedComponents
    ],
    providers: [
    ]
})
export class ErrorModule {
}
