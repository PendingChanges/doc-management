import { Component } from '@angular/core';

@Component({
  selector: 'app-document-page',
  standalone: true,
  imports: [],
  template: `<div class="p-5">
    <div class="grid">
      <div class="col-12 lg:col-6 xl:col-3">
        <div class="surface-card shadow-2 p-3 border-1 border-50 border-round">
          <div class="flex justify-content-between mb-3">
            <div>
              <span class="block text-500 font-medium mb-3">Documents</span>
              <div class="text-900 font-medium text-xl">Total: 152</div>
              <div class="text-900 font-medium text-xl">Mine: 53</div>
            </div>
          </div>
          <span class="text-green-500 font-medium">24 new </span
          ><span class="text-500">since last visit</span>
        </div>
      </div>
    </div>
  </div>`,
  styles: '',
})
export class DashboardPageComponent {}
