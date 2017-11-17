
    $(document).ready(function(){
        $('#DepartmentDropDown').check(function () {
            if ($("#DepartmentDropDown").val == "Select") {
                $('#CourseDropDown').prop('disabled', true);
            }
            else
                $('#CourseDropDown').prop('disabled', false);
        });
    });


