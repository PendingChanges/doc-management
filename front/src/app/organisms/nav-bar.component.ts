import { NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MenubarModule } from 'primeng/menubar';

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [RouterOutlet, MenubarModule, NgIf],
  template: `<p-menubar [model]="menubarItems">
    <ng-template pTemplate="item" let-item>
      <ng-container *ngIf="item.route; else urlRef">
        <a [routerLink]="item.route" class="p-menuitem-link">
          <span [class]="item.icon"></span>
          <span class="ml-2">{{ item.label }}</span>
        </a>
      </ng-container>
      <ng-template #urlRef>
        <a
          *ngIf="item.url; else noLink"
          [href]="item.url"
          class="p-menuitem-link"
        >
          <span [class]="item.icon"></span>
          <span class="ml-2">{{ item.label }}</span>
        </a>
      </ng-template>
      <ng-template #noLink>
        <div class="p-menuitem-link">
          <span [class]="item.icon"></span>
          <span class="ml-2">{{ item.label }}</span>
          <span class="pi pi-fw pi-angle-down ml-2"></span>
        </div>
      </ng-template>
    </ng-template>
  </p-menubar>`,
  styles: ``,
})
export class NavBarComponent implements OnInit {
  menubarItems: any[] | undefined;
  ngOnInit() {
    this.menubarItems = [];
  }
}
