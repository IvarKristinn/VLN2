// Write your JavaScript code.
// Instantiate the Bootstrap carousel


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
