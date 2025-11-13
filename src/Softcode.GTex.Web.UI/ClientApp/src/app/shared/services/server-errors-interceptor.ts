// server-errors.interceptor.ts
import { Injectable } from '@angular/core';
import
{
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
    HttpErrorResponse
} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/retry';
@Injectable()
export class ServerErrorsInterceptor implements HttpInterceptor
{

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>
    {
        return next.handle(request).catch((err: HttpErrorResponse) =>
        {
            return Observable.throw(err);
        });
    }
}
