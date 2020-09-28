# aspnetJatko
ASPNET jatkokurssi

Kun model vaikkapa viewModels kansioon on tehty niin sitten luodaan controller.

    MVC 5 with views using Entity Framework . Rasti ruutuun generate views .

    Controllerin päälle luodaan sitten view . Hiiren kakkosella editointialueella ei solution explorer alueella.

    Tämän jälkeen voikin jo kokeilla toimiiko esimerkiksi selailu. Jos controllerin nimi on esimerkikisi
    OrdersController niin /orders/index päättyvällä osoitteella saat näkymän selaimelle.

    Viewbag muuttujat kannattaa alustaa heti controllerin index osiossa.

    Jos halutaan jquerylla viitata modaali ikkunaan kuten  $('#login').modal('show'); niin se viittaa
     <div class="modal" id="login"> olevaan id kenttään. Formin sisällä olevat id loginForm välitetään
     buttonilla controllerin funktiolle.
