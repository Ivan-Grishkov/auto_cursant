<!DOCTYPE html>
<html>
<head>
    <% Response.StatusCode = 404 %>
    <% Response.Status = "404 Page Not Found" %>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>404 Not Found Error - uroki-vozhdeniya.by</title>
    <link href="/Content/bootstrap.css" rel="stylesheet"/>
    <link href="/Content/agency.css" rel="stylesheet"/>
    <link rel="shortcut icon" href="/favicon.ico" type="image/x-icon"/>
</head>
<body>
    <div class="error-container">
        <header class="no-bg">
            <div class="container">
                <div class="row">
                    <div class="col-lg-2 align-center">
                        <a class="btn btn-primary go-home" href="/">Уроки вождения</a>
                    </div>
                    <div class="col-lg-10 text-justify">
                        <h2 class="section-heading text-primary">404 ERROR</h2>
                        <h3 class="section-subheading text-right">
                            <i>Ресурс не найден</i>
                        </h3>
                    </div>
                </div>
            </div>
        </header>

        <div class="container">
            <div class="row center-content">
                <div class="col-md-12">
                    <a href="/">
                        <img class="header-logo" src="/img/not-a-lawyer-accident-cartoon.jpg" />
                    </a>
                </div>
            </div>
        </div>

        <div class="base-content row">
            <div class="base-error text-center col-md-12">
                Страница не найдена, вернитесь на <a href="/">главную</a>.
            </div>

            <hr />
        </div>
    </div>
</body>

</html>