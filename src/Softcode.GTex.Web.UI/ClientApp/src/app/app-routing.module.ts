import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AuthorizationGuard } from './shared/services/authorization.guard';
import { AuthorizationCanGuard } from './shared/services/authorization.can.guard';
import { CanDeactivateGuard } from './shared/can-deactivate/can-deactivate.guard';


const routes: Routes = [
    { path: '', loadChildren: './application/application.module#ApplicationModule', canActivate: [AuthorizationGuard], canLoad: [AuthorizationCanGuard] },
    { path: 'login', loadChildren: './login/login.module#LoginModule' },
    //{ path: 'silent-renew', redirectTo: 'silent-renew.html', pathMatch: 'full' }
    { path: 'itm-admin', loadChildren: './itm-admin/itm-admin.module#ItmAdminModule' },
    { path: '**', redirectTo: '/' }

];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
