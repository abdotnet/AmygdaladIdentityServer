import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { NgLoadingSpinnerModule, NgLoadingSpinnerInterceptor } from 'ng-loading-spinner';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CreateproductComponent } from './createproduct/createproduct.component';
import { EditproductComponent } from './editproduct/editproduct.component';
import { ListproductComponent } from './listproduct/listproduct.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { RegisteradminComponent } from './registeradmin/registeradmin.component';
import { NavbarComponent } from './navbar/navbar.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthguardGuard } from './_service/authguard.guard';
import { NavComponent } from './nav/nav.component';
import { NgxSpinnerModule } from "ngx-spinner";
import { NgxPaginationModule } from 'ngx-pagination';
import { ProducthistoryComponent } from './producthistory/producthistory.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent  },
  { path: 'dashboard', component: DashboardComponent,canActivate: [AuthguardGuard], data: {
    role: 'WorkerAdmin'
  } },
  { path: 'product', component: CreateproductComponent,canActivate: [AuthguardGuard] ,data: {
    role: 'Worker'
  }},
  { path: 'edit-product/:id', component: EditproductComponent,canActivate: [AuthguardGuard], data: {
    role: 'Worker'
  }},
  { path: 'list-product', component: ListproductComponent,canActivate: [AuthguardGuard],data: {
    role: 'Worker'
  } },
  { path: 'product-history', component: ProducthistoryComponent,canActivate: [AuthguardGuard],data: {
    role: 'Admin'
  } },
  { path: 'register', component: RegisterComponent , data: {
    role: 'WorkerAdmin'
  }},
  { path: 'app', component: AppComponent, data: {
    role: 'WorkerAdmin'
  } },
  { path: '',   redirectTo: '/login', pathMatch: 'full' },
  { path: '**', component: LoginComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    CreateproductComponent,
    EditproductComponent,
    ListproductComponent,
    LoginComponent,
    RegisterComponent,
    RegisteradminComponent,
    NavbarComponent,
    DashboardComponent,
    NavComponent,
    ProducthistoryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    NgxSpinnerModule,
    HttpClientModule,
    NgxPaginationModule,
    RouterModule.forRoot(routes),
    ToastrModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          return localStorage.getItem("access_token");
        },
        allowedDomains: ["https://localhost:44395"],
        disallowedRoutes: ["http://example.com/examplebadroute/"],
      },
    }) // ToastrModule added
  ],
  providers: [ { provide: HTTP_INTERCEPTORS, useClass: NgLoadingSpinnerInterceptor, multi: true }
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule { }
