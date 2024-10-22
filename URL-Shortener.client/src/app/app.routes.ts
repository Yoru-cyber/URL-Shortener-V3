import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { ShortenerComponent } from './pages/shortener/shortener.component';

export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  {path: 'shortener', component: ShortenerComponent}
];
