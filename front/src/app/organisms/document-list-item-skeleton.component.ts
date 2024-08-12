import { CommonModule } from '@angular/common';
import { Component, input } from '@angular/core';

import { SkeletonModule } from 'primeng/skeleton';

@Component({
  selector: 'app-document-list-item-skeleton',
  standalone: true,
  imports: [CommonModule, SkeletonModule],
  template: `<div
    class="flex flex-column sm:flex-row sm:align-items-center p-4 gap-3"
    [ngClass]="{ 'border-top-1 surface-border': !first() }"
  >
    <div class="md:w-10rem relative">
      <p-skeleton size="5rem" />
    </div>
    <div
      class="flex flex-column md:flex-row justify-content-between md:align-items-center flex-1 gap-4"
    >
      <div style="flex: 1">
        <p-skeleton width="100%" styleClass="mb-2" />
        <p-skeleton width="75%" />
      </div>
      <div class="flex flex-column md:align-items-end gap-5">
        <div class="flex flex-row-reverse md:flex-row gap-2">
          <p-skeleton size="3rem" styleClass="mr-2" />
        </div>
      </div>
    </div>
  </div>`,
  styles: '',
})
export class DocumentListItemSkeletonComponent {
  first = input<boolean>();
}
