import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ItmAdminComponent } from './itm-admin.component';
import { DashboardComponent } from '../itm-admin/dashboard/dashboard.component';
import { ListPageDetailComponent } from './list-page-detail/list-page-detail.component';
import { ListPageListComponent } from './list-page-list/list-page-list.component';

 

 

const routes: Routes = [
    {
        path: '',
        component: ItmAdminComponent,
        children: [
            { path: '', redirectTo: 'dashboard', pathMatch: "full" },
            { path: 'dashboard', component: DashboardComponent, data: { title: 'Dashboard' } },
            { path: 'list-page', component: ListPageDetailComponent, data: { title: 'List Page' } }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ItmAdminRouterModule{

}

export const RoutedComponents = [
    ItmAdminComponent
    , DashboardComponent
    , ListPageDetailComponent
    , ListPageListComponent
];
