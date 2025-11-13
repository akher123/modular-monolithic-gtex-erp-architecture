import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CompanyFormComponent } from './components/company/company-form.component'
import { CrmComponent } from './crm.component'
import { ContactDetailComponent } from './components/contact/contact-detail.component'
import { ContactFormComponent } from './components/contact/contact-form.component'
import { ListComponent } from './../application-shared/components/list/list.component';
import { CompanyDetailComponent } from './components/company/company-detail.component';

const routes: Routes = [{
    path: '', component: CrmComponent,
    children: [
        { path: 'companies', component: ListComponent, data: { title: 'Companies' } },
        { path: 'company', component: CompanyDetailComponent, data: { title: 'New Company' } },
        { path: 'company/:companyId', component: CompanyDetailComponent, data: { title: 'Modify Company' } },
        { path: 'contact', component: ContactDetailComponent, data: { title: 'Contact Detail' } },
        { path: 'contact/:contactId', component: ContactDetailComponent, data: { title: 'Contact Detail' } },
        { path: 'contacts', component: ListComponent, data: { title: 'Contacts' } },
    ],
}];


@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CrmRoutingModule {
}

export const RoutedComponents = [
    CrmComponent
    , CompanyFormComponent
    , ContactDetailComponent
    , ContactFormComponent
    , CompanyDetailComponent
];

