// Write your JavaScript code.
// Instantiate the Bootstrap carousel
$('.multi-item-carousel').carousel({
    interval: false
  });
  
  // for every slide in carousel, copy the next slide's item in the slide.
  // Do the same for the next, next item.
  $('.multi-item-carousel .item').each(function(){
    var next = $(this).next();
    if (!next.length) {
      next = $(this).siblings(':first');
    }
    next.children(':first-child').clone().appendTo($(this));
    
    if (next.next().length>0) {
      next.next().children(':first-child').clone().appendTo($(this));
    } else {
        $(this).siblings(':first').children(':first-child').clone().appendTo($(this));
    }
  });

$("#savedBillingGet").click(function(){
  var i = $("select#SavedBillingAddresses").val();

  var streetId = "#Street-" + i;
  var selectedStreet = $(streetId).text();

  var HouseNumId = "#HouseNum-" + i;
  var selectedHouseNum = $(HouseNumId).text();

  var CityId = "#City-" + i;
  var selectedCity = $(CityId).text();

  var ZipCodeId = "#ZipCode-" + i;
  var selectedZipCode = $(ZipCodeId).text();

  var CountryId = "#Country-" + i;
  var selectedCountry = $(CountryId).text();

  $("#billStreet").val(selectedStreet);
  $("#billHouseNum").val(selectedHouseNum);
  $("#billCity").val(selectedCity);
  $("#billZipCode").val(selectedZipCode);

  $("#billCountrySelect option[value=" + selectedCountry + "]").attr('selected', 'selected');
});

$("#savedShippingGet").click(function(){
  var i = $("select#SavedShippingAddresses").val();

  var streetId = "#Street-" + i;
  var selectedStreet = $(streetId).text();

  var HouseNumId = "#HouseNum-" + i;
  var selectedHouseNum = $(HouseNumId).text();

  var CityId = "#City-" + i;
  var selectedCity = $(CityId).text();

  var ZipCodeId = "#ZipCode-" + i;
  var selectedZipCode = $(ZipCodeId).text();

  var CountryId = "#Country-" + i;
  var selectedCountry = $(CountryId).text();

  $("#shippStreet").val(selectedStreet);
  $("#shippHouseNum").val(selectedHouseNum);
  $("#shippCity").val(selectedCity);
  $("#shippZipCode").val(selectedZipCode);

  $("#shippCountrySelect option[value=" + selectedCountry + "]").attr('selected', 'selected');
});
