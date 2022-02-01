


$(function () {
    jQuery.validator.addMethod('checkvalidweight', function (value, element, params) {
        //  var currentDate = new Date();
       
        alert("value:" + $(params).val());
        console.log("value: " + value);
        var weightunit = $(params).val();

        console.log("weightunit: " + weightunit);

        alert("weight unit:" + weightunit);
        debugger;
        if (value < 18 || value > 100 || !$.isNumeric(value)) {
            return false;
        }
        return true;
    }, '');
    jQuery.validator.unobtrusive.adapters.add('checkvalidweight', function (options) {
        options.rules['checkvalidweight'] = {};
        options.messages['checkvalidweight'] = options.message;
    });

  

}(jQuery));