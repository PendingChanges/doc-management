import { CommonModule, NgFor } from '@angular/common';
import { Component, computed, input } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { DataViewModule } from 'primeng/dataview';
import { TagModule } from 'primeng/tag';
import { ImageModule } from 'primeng/image';

import { Document } from '../../graphql/generated';
import { environment } from '../../infrastructure/environments/environment.development';

@Component({
  selector: 'app-document-icon',
  standalone: true,
  imports: [ImageModule],
  template: ` <p-image
    class="block xl:block mx-auto border-round w-full"
    [src]="iconUrl()"
    [alt]="document()?.name"
    width="30"
  />`,
  styles: '',
})
export class DocumentIconComponent {
  document = input<Document>();

  iconUrl = computed(() => {
    switch (this.document()?.extension) {
      case '.pdf':
        return '/pdf.svg';
      default:
        return '/unknown.svg';
    }
  });
}
