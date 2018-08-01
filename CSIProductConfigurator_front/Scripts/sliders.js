function initSliders() {
    $("#CPWSlider").slider({
        value: 0,
        min: 0,
        max: 500,
        step: 1,

        formatter: function (value) {
            return "£" + value;
        }
    });

    $("#CPWSlider").on("slide slideStop", function (slider) {
        update(slider);
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


    $("#Tier1StorageSlider").on("slide slideStop", function (slider) {
        update(slider);
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


    $("#Tier2StorageSlider").on("slide slideStop", function (slider) {
        update(slider);
    });

};

function update(slider) {

    var whichSlider = slider.target.id;

    

    $('#' + whichSlider + "Box").val(slider.value);

    var CPW = $("#CPWSliderBox").val();
    var Tier1 = $("#Tier1StorageSliderBox").val();

    var totalCost = (CPW * Tier1);

    alert(CPW + Tier1 + totalCost);

    /*
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
    */
}