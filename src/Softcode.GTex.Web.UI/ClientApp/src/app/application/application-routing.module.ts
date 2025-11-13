import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ApplicationComponent } from './application.component';
import { DashboardComponent } from './dashboard/dashboard.component'

const routes: Routes = [
    {
        path: '',
        component: ApplicationComponent,
        children: [
            { path: '', redirectTo: 'dashboard', pathMatch: "full" },
            { path: 'dashboard', component: DashboardComponent, data: { title: 'Dashboard' } },
            { path: 'crm', loadChildren: './crm/crm.module#CrmModule' },
            { path: 'hrm', loadChildren: './hrm/hrm.module#HrmModule' },
            { path: 'dms', loadChildren: './dms/dms.module#DmsModule' },
            { path: 'communication', loadChildren: './communication/communication.module#CommunicationModule' },
            { path: 'system-settings', loadChildren: './system-settings/system-settings.module#SystemSettingsModule' },
            { path: 'error', loadChildren: './error/error.module#ErrorModule' }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ApplicationRoutingModule {

}

