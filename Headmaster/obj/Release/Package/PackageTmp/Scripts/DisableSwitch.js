
    $(document).ready(function(){
        $('#DepartmentDropDown').check(function () {
            if ($("#DepartmentDropDown").val == "Select") {
                $('#State').prop('disabled', true);
            }
            else
                $('#State').prop('disabled', false);
        });
    });


