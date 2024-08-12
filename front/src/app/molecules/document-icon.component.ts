import { Component, computed, input } from '@angular/core';
import { ImageModule } from 'primeng/image';

import { Document } from '../../graphql/generated';

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
