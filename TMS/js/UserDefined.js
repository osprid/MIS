function ValidateRequiredField() {
    try {
        var isValid = true;
        $('.required').each(function () {
            if ($.trim($(this).val()) == '') {
                isValid = false;
                $(this).css({
                    "border": "1px solid red",
                    "background": "#FFCECE"
                });
                var elm = '<div class="errormsg" style="color:red">required field</div>';
                $(elm).appendTo(this.container);
            }
            else {
                $(this).css({
                    "border": "",
                    "background": ""
                });
            }
        });
        if (isValid == false)
            e.preventDefault();
        else {
            return true;
        }
    }
    catch (err) {
        return false;
    }
}