import { CommonModule } from '@angular/common';
import { Component, computed, input } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { ToolbarModule } from 'primeng/toolbar';

import { Document } from '../../graphql/generated';
import { environment } from '../../infrastructure/environments/environment.development';
import { DocumentIconComponent } from '../molecules/document-icon.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-document-list-item',
  standalone: true,
  imports: [
    ButtonModule,
    DocumentIconComponent,
    CommonModule,
    ToolbarModule,
    RouterModule,
  ],
  template: ` <a
    [routerLink]="documentInfosUrl()"
    rel="noopener noreferrer"
    class="no-underline"
    ><div
      class="flex flex-column sm:flex-row sm:align-items-center hover:surface-100 cursor-pointer"
      [ngClass]="{ 'border-top-1 surface-border': !first() }"
    >
      <div class="md:w-10rem relative">
        <app-document-icon [document]="document()" />
      </div>
      <div
        class="flex flex-column md:flex-row justify-content-between md:align-items-center flex-1"
      >
        <div
          class="flex flex-row md:flex-column justify-content-between align-items-start gap-2 text-lg font-medium text-900"
        >
          {{ document()?.name }}
        </div>
        <div class="flex flex-column md:align-items-end">
          <div class="flex flex-row-reverse md:flex-row">
            <a
              [href]="documentUrl()"
              target="_blank"
              rel="noopener noreferrer"
              class="p-button mr-2"
            >
              <i class="pi pi-external-link"></i>
            </a>
          </div>
        </div>
      </div></div
  ></a>`,
  styles: '',
})
export class DocumentListItemComponent {
  document = input<Document>();
  first = input<boolean>();
  documentUrl = computed(
    () => `${environment.apiUrl}/documents/${this.document()?.id}`
  );
  documentInfosUrl = computed(() => `/documents/${this.document()?.id}`);
}
