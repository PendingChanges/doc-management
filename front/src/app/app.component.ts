import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { NavBarComponent } from './organisms/nav-bar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavBarComponent, RouterModule],
  template: `<div class="min-h-screen flex relative lg:static surface-ground">
    <div
      class="surface-section h-full lg:h-auto hidden lg:block flex-shrink-0 absolute lg:static left-0 top-0 z-1 border-right-1 surface-border select-none"
    >
      <div
        class="flex align-items-center px-5 flex-shrink-0"
        style="height: 60px;"
      >
        Document Management
      </div>
      <div class="overflow-y-auto">
        <ul class="list-none p-3 m-0">
          <li>
            <ul class="list-none p-0 m-0 overflow-hidden" style="">
              <li>
                <a
                  [routerLink]="'/dashboard'"
                  pripple=""
                  class="no-underline	p-ripple p-element flex align-items-center cursor-pointer p-3 border-round text-700 hover:surface-100 transition-duration-150 transition-colors"
                  ><i class="pi pi-home mr-2"></i
                  ><span class="font-medium">Dashboard</span
                  ><span
                    class="p-ink"
                    style="height: 247px; width: 247px; top: -101.9px; left: -6.3px;"
                  ></span
                ></a>
              </li>
              <li>
                <a
                  [routerLink]="'/documents'"
                  pripple=""
                  class="no-underline	p-ripple p-element flex align-items-center cursor-pointer p-3 border-round text-700 hover:surface-100 transition-duration-150 transition-colors"
                  ><i class="pi pi-bookmark mr-2"></i
                  ><span class="font-medium">Documents</span
                  ><span class="p-ink"></span
                ></a>
              </li>
            </ul>
          </li>
        </ul>
      </div>
    </div>
    <div class="min-h-screen flex flex-column relative flex-auto">
      <div
        class="header flex justify-content-between align-items-center px-5 surface-0 border-bottom-1 surface-border relative lg:static"
        style="height: 60px;"
      >
        <div class="flex">
          <a
            pripple=""
            pstyleclass="#app-sidebar"
            enterclass="hidden"
            enteractiveclass="fadeinleft"
            leavetoclass="hidden"
            leaveactiveclass="fadeoutleft"
            class="p-element p-ripple cursor-pointer block lg:hidden text-700 mr-3"
            ><i class="pi pi-bars text-4xl"></i><span class="p-ink"></span></a
          ><span class="p-input-icon-left" data-dashlane-rid="ce71f8c36bef27a0"
            ><i class="pi pi-search"></i
            ><input
              type="text"
              pinputtext=""
              placeholder="Search"
              class="p-inputtext p-component p-element border-none w-10rem sm:w-20rem"
              data-dashlane-rid="3ffa518583615344"
          /></span>
        </div>
        <a
          pripple=""
          pstyleclass="@next"
          enterclass="hidden"
          enteractiveclass="fadein"
          leavetoclass="hidden"
          leaveactiveclass="fadeout"
          class="p-element p-ripple cursor-pointer block lg:hidden text-700"
          ><i class="pi pi-ellipsis-v text-2xl"></i><span class="p-ink"></span
        ></a>
        <ul
          class="list-none p-0 m-0 hidden lg:flex lg:align-items-center select-none lg:flex-row surface-section border-1 lg:border-none surface-border right-0 top-100 z-1 shadow-2 lg:shadow-none absolute lg:static"
        >
          <li>
            <a
              pripple=""
              class="p-ripple p-element flex p-3 lg:px-3 lg:py-2 align-items-center text-600 hover:text-900 hover:surface-100 font-medium border-round cursor-pointer transition-duration-150 transition-colors"
              ><i class="pi pi-inbox text-base lg:text-2xl mr-2 lg:mr-0"></i
              ><span class="block lg:hidden font-medium">Inbox</span
              ><span class="p-ink"></span
            ></a>
          </li>
          <li>
            <a
              pripple=""
              class="p-ripple p-element flex p-3 lg:px-3 lg:py-2 align-items-center text-600 hover:text-900 hover:surface-100 font-medium border-round cursor-pointer transition-duration-150 transition-colors"
              ><i
                pbadge=""
                severity="danger"
                class="p-element pi pi-bell text-base lg:text-2xl mr-2 lg:mr-0 p-overlay-badge"
                ><span
                  id="pr_id_1_badge"
                  class="p-badge p-component p-badge-danger p-badge-dot"
                ></span></i
              ><span class="block lg:hidden font-medium">Notifications</span
              ><span class="p-ink"></span
            ></a>
          </li>
          <li class="border-top-1 surface-border lg:border-top-none">
            <a
              pripple=""
              class="p-ripple p-element flex p-3 lg:px-3 lg:py-2 align-items-center hover:surface-100 font-medium border-round cursor-pointer transition-duration-150 transition-colors"
              ><img
                src="assets/images/blocks/avatars/circle/avatar-f-1.png"
                class="mr-3 lg:mr-0"
                style="width: 32px; height: 32px;" />
              <div class="block lg:hidden">
                <div class="text-900 font-medium">Josephine Lillard</div>
                <span class="text-600 font-medium text-sm"
                  >Marketing Specialist</span
                >
              </div>
              <span class="p-ink"></span
            ></a>
          </li>
        </ul>
      </div>
      <div class="flex flex-column flex-auto">
        <router-outlet></router-outlet>
      </div>
    </div>
  </div>`,
  styles: `.sidebar {
    width: 250px;
}
.sidebar h2 {
    text-align: center;
    color: #000;
}

.sidebar ul {
    list-style-type: none;
    padding: 0;
}

.sidebar ul li {
    padding: 10px;
    text-align: center;
}

.sidebar ul li a {
    color: white;
    text-decoration: none;
    display: block;
    transition: background-color 0.3s;
}

.sidebar ul li a:hover {
    background-color: #575757;
}


.header{
  height: 60px;
}
`,
})
export class AppComponent {
  title = 'front';
  sidebarVisible = true;
}
