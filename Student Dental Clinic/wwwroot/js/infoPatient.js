
     $(document).ready(function () {
         // إظهار الفورم عند الضغط
         $('#startBtn').on('click', function() {
             $('#patientFormSection').fadeIn(600).css("display", "block");
             $('html, body').animate({
                 scrollTop: $("#patientFormSection").offset().top - 20
             }, 600);
         });

         // تبديل صندوق الحساسية
         $('input[name="hasAllergy"]').on('change', function () {
             if ($(this).val() === 'yes') {
                 $('#allergyDetailsBox').removeClass('d-none').hide().fadeIn();
             } else {
                 $('#allergyDetailsBox').fadeOut(function() { $(this).addClass('d-none'); });
             }
         });

         // تأكيد الإرسال
         $('#patientForm').on('submit', function (e) {
             e.preventDefault();
             Swal.fire({
                 title: 'Success!',
                 text: 'Your request has been submitted.',
                 icon: 'success',
                 confirmButtonColor: '#198754'
             }).then((result) => {
                 if (result.isConfirmed) this.submit();
             });
         });
     });
