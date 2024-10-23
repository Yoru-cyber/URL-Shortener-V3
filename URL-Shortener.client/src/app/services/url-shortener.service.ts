import { Injectable } from '@angular/core';
import ShortenedUrlDto from '../Dtos/ShortenedUrl';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class UrlShortenerService {
private httpClient: HttpClient;
private apiUrl = "http://localhost:8080/api/v1/Shortener/encode";
  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
   }
  public createShortenedUrl(Url: string): Observable<ShortenedUrlDto> {
    return this.httpClient.post<ShortenedUrlDto>(this.apiUrl, {Url: Url});
  }
}
