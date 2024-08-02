import { Routes } from '@angular/router';
import { DocumentsPageComponent } from './pages/documents-page.component';

export const routes: Routes = [{
    path: 'documents',
    component: DocumentsPageComponent,
},
{
    path: '',
    pathMatch: 'full',
    redirectTo: 'documents',
},];
