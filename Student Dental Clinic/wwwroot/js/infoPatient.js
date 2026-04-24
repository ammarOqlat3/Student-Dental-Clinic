$(document).ready(function () {
    // 1. Show Form
    $('#startBtn').on('click', function (e) {
        e.preventDefault();
        $('#patientFormSection').fadeIn(600).css("display", "block");
        $('html, body').animate({
            scrollTop: $("#patientFormSection").offset().top - 20
        }, 600);
    });

    // 2. Allergy Toggle
    $('input[name="hasAllergy"]').on('change', function () {
        if ($(this).val() === 'yes') {
            $('#allergyDetailsBox').removeClass('d-none').hide().fadeIn();
        } else {
            $('#allergyDetailsBox').fadeOut(function () {
                $(this).addClass('d-none');
            });
        }
    });

    // 3. Form Submit
    $('#patientForm').on('submit', function (e) {
        e.preventDefault();
        var form = this;
        Swal.fire({
            title: 'Success!',
            text: 'Your request has been submitted.',
            icon: 'success',
            confirmButtonColor: '#198754'
        }).then((result) => {
            if (result.isConfirmed) {
                form.submit();
            }
        });
    });
});