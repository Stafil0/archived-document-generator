# Задание по генерации документов
## Цель
Изучить возможность применения ООП, SOLID и паттернов проектирования для создания инструмента генерации документов с жесткой формой.

## Дано
Существует некоторая разметка для генерации документов. Она представлена в виде json-файла, например:

```
{  
    "layout":[  
        {  
            "type":"date",
            "location":{  
                "left":963,
                "top":9,
                "width":419,
                "height":100
            },
            "value":"01.12.2018",
            "format":"{0:dd.MM.yy}",
            "alignmentX":"middle",
            "alignmentY":"middle",
            "rotation":0
        },
        {  
            "type":"text",
            "location":{  
                "left":1244,
                "top":177,
                "width":134,
                "height":1555
            },
            "value":"Акционерное общество «Рога и копыта» ИНН 1234567890  КПП 11101000  р/сч. 4424444444444444 к/сч.  3333333333333333333 ФИЛИАЛ \"ИЖЕВСКИЙ\" ОАО \"СБЕРБАНК\" БИК  04158579875 426000, г. Ижевск, пер. Северный, e-mail: email@mail.mail",
            "format":"{0}",
            "alignmentX":"middle",
            "alignmentY":"middle",
            "rotation":90
        },
        {  
            "type":"text",
            "location":{  
                "left":49,
                "top":262,
                "width":182,
                "height":51
            },
            "value":"Исх. № 25 от",
            "format":"{0}",
            "alignmentX":"right",
            "alignmentY":"middle",
            "rotation":0
        },
        {  
            "type":"date",
            "location":{  
                "left":253,
                "top":261,
                "width":280,
                "height":51
            },
            "value":"29.11.2018",
            "format":"{0:dd.MM.yy}",
            "alignmentX":"left",
            "alignmentY":"middle",
            "rotation":0
        },
        {  
            "type":"text",
            "location":{  
                "left":284,
                "top":339,
                "width":754,
                "height":121
            },
            "value":"Уважаемый Геннадий Александрович!",
            "format":"{0}",
            "alignmentX":"middle",
            "alignmentY":"middle",
            "rotation":0
        },
        {  
            "type":"text",
            "location":{  
                "left":59,
                "top":492,
                "width":1150,
                "height":473
            },
            "value":"18-21 мая 2016 года в Санкт-Петербурге состоялся очередной Петербургский Международный Юридический Форум (далее – Форум), который ежегодно собирает более 3500 представителей международных организаций, органов государственной власти, юридического бизнеса, науки из более чем 80 стран мира различных континентов. От имени ПАО «Консалт-Плюс» (далее – Общество) благодарю Вас за возможность принять участие в этом крупном мероприятии, организованном на самом высоком уровне. В рамках проведения Форума 20 мая 2016 года состоялся круглый стол Ассоциации «Модель функционирования национальной Ассоциации юристов: лучшие реализованные проекты», в работе которого приняли участие представители национальных Ассоциаций из России, Индии, Китая, Белоруссии и США. Участники обменялись опытом реализации лучших проектов национальных Ассоциаций в сфере оказания бесплатной юридической помощи, общественной аккредитации образовательных программ, правового просвещения населения и работы с молодежью. Благодарим Вас за возможность проведения данного мероприятия в рамках Форума и надеемся на дальнейшее плодотворное сотрудничество.",
            "format":"{0}",
            "alignmentX":"left",
            "alignmentY":"top",
            "rotation":0
        },
		{  
            "type":"text",
            "location":{  
                "left":482,
                "top":1037,
                "width":724,
                "height":160
            },
            "value":"Прошу вас написать «Рекомендательное письмо» о нашей организации и качеству проделанной работе за время нашего сотрудничества. Буду очень признателен.",
            "format":"{0}",
            "alignmentX":"left",
            "alignmentY":"top",
            "rotation":0
        },
        {  
            "type":"image",
            "location":{  
                "left":71,
                "top":1148,
                "width":382,
                "height":345
            },
            "value":".\\stamps\\1.jpg",
            "alignmentX":"middle",
            "alignmentY":"middle",
            "rotation":0
        },
        {  
            "type":"text",
            "location":{  
                "left":474,
                "top":1416,
                "width":272,
                "height":87
            },
            "value":"Президент",
            "format":"{0}",
            "alignmentX":"left",
            "alignmentY":"middle",
            "rotation":0
        },
        {  
            "type":"text",
            "location":{  
                "left":958,
                "top":1416,
                "width":269,
                "height":86
            },
            "value":"Н.А. Уловкин",
            "format":"{0}",
            "alignmentX":"right",
            "alignmentY":"middle",
            "rotation":0
        },
        {  
            "type":"text",
            "location":{  
                "left":22,
                "top":1922,
                "width":309,
                "height":53
            },
            "value":"Страница 1 из 1",
            "format":"{0}",
            "alignmentX":"middle",
            "alignmentY":"middle",
            "rotation":0
        },
        {  
            "type":"date",
            "location":{  
                "left":1056,
                "top":1921,
                "width":320,
                "height":55
            },
            "value":"01.12.2018",
            "format":"{0:dd.MM.yy}",
            "alignmentX":"middle",
            "alignmentY":"middle",
            "rotation":0
        }
    ],
    "shift":0,
    "rotate":0,
    "size":{  
        "width":1393,
        "height":2009
    }
}
```

### Пояснение:
-	Type – тип объекта (текст/дата/картинка);
-	Location – позиция объекта разметки;
-	Left – позиция верхнего левого угла по X (в пикселях);
-	Top – позиция верхнего левого угла по Y (в пикселях);
-	Width – ширина объекта разметки;
-	Height – высота объекта разметки;
-	Value – значение объекта разметки;
-	Format – формат объекта, доступно только для текста и даты;
-	AlignmentX – выравнивание по X (left/middle/right);
-	AlignmentY – выравнивание по Y (left/middle/right);
-	Rotation – градус поворота объекта (0/90/180/270);
-	Size – размер страницы.

При разработке желательно следовать принципам [ООП](https://metanit.com/sharp/tutorial/3.1.php) и 
[SOLID](https://metanit.com/sharp/patterns/5.1.php) и, 
по возможности, использовать [паттерны проектирования](https://refactoring.guru/ru/design-patterns) 
([еще одна ссылка](https://metanit.com/sharp/patterns/1.1.php)).

## Задачи
-	Настроить рабочее место. Прочитать по SOLID, ООП, паттернам.
-	Разметку и маску документа взять примеры выше. Разметку можно менять, если она не соответствует требованию разделения ответственностей (напр. какие-то поля находятся не в том классе);
-	Перед разработки, желательно спроектировать и обсудить архитектуру приложения;
-	Создать консольную программу, которая будет считывать данные из json-разметки;
-	Добавить нанесение информации на маску документа исходя из разметки;
-	Добавить рандомизацию при нанесении информации на документ (напр. сдвиги позиций, повороты маски);
-	Создать консольную программу для генерации json-разметки:
-	для каждого типа блока разметки должна существовать возможность выбора случайных значений (напр. из файла или по regex);
-	считывание данных для генерации разметки должно происходить из json-файла.
-	Добавить в генератор разметки указания шрифта, цвета шрифта и размера шрифта для текста и даты;
- *Добавить в генератор json-разметки возможность добавления в качестве блоков уже существующую json-разметку.

## Теоретические вопросы
-	Зачем вообще нужно ООП? Почему нельзя писать всё “как попало”?
-	Как правильно читать json-файлы? Что такое десериализация объектов?
-	Какие из паттернов проектирования были применены при разработке? Какие можно было бы применить?
-	Какими типами данных еще можно дополнить разметку?
-	В каком еще виде можно хранить данные для разметки? Как было бы удобнее на твой взгляд?
-	Предложи свой вариант хранения данных для генерации разметки.

## Пояснения
Стек инструментов: Visual Studio 2017/2019 Community (Разработка классических приложений .NET), .NET Framework 4.7.2.

Для решения задач можно использовать абсолютно любые источники получения информации.

Результат завершения каждой задачи необходимо показать руководителю практики, объяснить полученные результаты, обсудить следующую задачу.
