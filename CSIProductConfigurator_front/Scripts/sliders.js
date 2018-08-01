function initSliders()
{
    $("#CPWSlider").slider({
        value: 0,
        min: 0,
        max: 500,
        step: 1,
        
        formatter: function (value) {
            return "£" + value;
        }
    });

    $("#Tier1StorageSlider").slider({
        value: 0,
        min: 0,
        max: 1500,
        step: 1,
        
        formatter: function (value) {
            return "$" + value;
        }
    });

    $("#Tier2StorageSlider").slider({
        value: 0,
        min: 0,
        max: 5000,
        step: 50,
        
        formatter: function (value) {
            return "€" + value;
        }
    });

    

};

$(document).ready(function () {
    $("#monthsSlider").slider({
        value: 1,
        min: 1,
        max: 12,
        step: 1,
        tooltip: 'always'
    });

    $("#monthlyCostSlider").slider({
        value: 0,
        min: 0,
        max: 500,
        step: 1,
        tooltip: 'always',
        formatter: function (value) {
            return "£" + value;
        }
    });

    $("#monthsSlider").on("slide slideStop", function (slider) {
        update(slider);
    });

    $("#monthlyCostSlider").on("slide slideStop", function (slider) {
        update(slider);
    });

});
      
      function update(slider) {

          var whichSlider = slider.target.id;

          if (whichSlider == 'monthsSlider') {
              
              $("#panelMonths").html(slider.value);
          }

          if (whichSlider == 'monthlyCostSlider') {
              
              $("#panelMonthlyCost").html(slider.value);
          }

          var monthlyCost = $("#panelMonthlyCost").text();
          var months = $("#panelMonths").text();
          var totalCost = (monthlyCost * months);

          $("#panelTotalCost").text(totalCost);
         
      }