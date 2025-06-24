# FuturesClean.API
## Описание проекта

FuturesClean.API web api микросервис на архитектре **Code Excellence**  
Для создания, просмотра и удаление сущности **FuturesDifference**

(Для актуальных данных биржы используется API к *Binance*)
```CSharp
public class FuturesDifference
{
    public Guid? Id { get; set; }
    public DateTime TimeMeasuredUtc { get; set; }
    public string Interval { get; set; }
    public string SymbolCurrent { get; set; }
    public string SymbolNext { get; set; }
    public decimal Spread { get; set; }
}
```

+ ASP WEB API + Swagger для просмотра и тестирования функциональности
+ База данных PostgreSQL + Миграции

## Особенности функцианальности функциональности
- Автоматическое создание базы с нужной структурой через **IHostedService**
- Доступ к базе через **Repository Pattern**
- Глобальный отлов ошибок через **Midleware**
- Структура внутреннего ответа для общения сервисов

Реализованы контроллеры для вызова функциональности
### Base REST endpoint Controller
```bash
curl -X 'POST' \
  'https://localhost:7009/BaseEndPoint/FuturesDifference?interval=1h&utcTime=2025-05-23T00%3A00%3A00&symbols=BTCUSD' \
  -H 'accept: */*' \
  -d ''
```
```bash
curl -X 'GET' \
  'https://localhost:7009/BaseEndPoint/FuturesDifference' \
  -H 'accept: */*'
```
```bash
curl -X 'DELETE' \
  'https://localhost:7009/BaseEndPoint/FuturesDifference/0197a11f-521d-7ca1-9d60-794d080f3e9d' \
  -H 'accept: */*'
```
