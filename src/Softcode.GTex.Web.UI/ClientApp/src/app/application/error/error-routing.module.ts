import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ErrorComponent } from './error.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';

const routes: Routes = [{
    path: '', component: ErrorComponent,
    children: [
        { path: 'forbidden', component: ForbiddenComponent, data: { title: 'forbidden' } }
    ]
}];


@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ErrorRoutingModule {
}


export const RoutedComponents = [
    ErrorComponent,
    ForbiddenComponent
];
