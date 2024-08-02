import { CommonModule, NgFor } from '@angular/common';
import { Component, computed, input } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { DataViewModule } from 'primeng/dataview';
import { TagModule } from 'primeng/tag';
import { ImageModule } from 'primeng/image';

import { Document } from '../../graphql/generated';
import { environment } from '../../infrastructure/environments/environment.development';
import { DocumentIconComponent } from '../molecules/document-icon.component';

@Component({
  selector: 'app-document-list-item',
  standalone: true,
  imports: [ButtonModule, DocumentIconComponent, CommonModule],
  template: `<div
    class="flex flex-column sm:flex-row sm:align-items-center p-4 gap-3"
    [ngClass]="{ 'border-top-1 surface-border': !first() }"
  >
    <div class="md:w-10rem relative">
      <app-document-icon [document]="document()" />
    </div>
    <div
      class="flex flex-column md:flex-row justify-content-between md:align-items-center flex-1 gap-4"
    >
      <div
        class="flex flex-row md:flex-column justify-content-between align-items-start gap-2"
      >
        <div>
          <div class="text-lg font-medium text-900 mt-2">
            <a [href]="documentUrl()">{{ document()?.name }}</a>
          </div>
        </div>
      </div>
      <div class="flex flex-column md:align-items-end gap-5">
        <div class="flex flex-row-reverse md:flex-row gap-2">
          <p-button icon="pi pi-heart" [outlined]="true" />
        </div>
      </div>
    </div>
  </div>`,
  styles: '',
})
export class DocumentListItemComponent {
  document = input<Document>();
  first = input<boolean>();
  documentUrl = computed(
    () => `${environment.apiUrl}/documents/${this.document()?.id}`
  );
}
