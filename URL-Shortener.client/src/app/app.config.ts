import {
  ApplicationConfig,
  provideZoneChangeDetection,
  importProvidersFrom,
} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { en_US, provideNzI18n } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { FormsModule } from '@angular/forms';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient } from '@angular/common/http';
import { IconDefinition } from '@ant-design/icons-angular';
import { provideNzIcons } from 'ng-zorro-antd/icon';
import {
  GithubOutline,
  LinkedinOutline,
  EditOutline
} from '@ant-design/icons-angular/icons';
registerLocaleData(en);
const icons: IconDefinition[] = [GithubOutline, LinkedinOutline, EditOutline];
export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideNzI18n(en_US),
    importProvidersFrom(FormsModule),
    provideAnimationsAsync(),
    provideHttpClient(),
    provideNzIcons(icons),
  ],
};
