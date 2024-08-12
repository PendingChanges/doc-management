import { Routes } from '@angular/router';
import { DocumentsPageComponent } from './pages/documents-page.component';
import { DocumentPageComponent } from './pages/document-page.component';
import { DashboardPageComponent } from './pages/dashboard-page.component';

export const routes: Routes = [
  {
    path: 'dashboard',
    component: DashboardPageComponent,
  },
  {
    path: 'documents',
    component: DocumentsPageComponent,
  },
  {
    path: 'documents/:id',
    title: 'Documents',
    component: DocumentPageComponent,
  },
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'dashboard',
  },
];
