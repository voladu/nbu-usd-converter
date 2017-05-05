// Write your Javascript code.

    $( function() {
        $("#datepicker").datepicker({
            showOtherMonths: true,
            selectOtherMonths: true,
            changeMonth: true,
            changeYear: true,
            showAnim: "slideDown",
            maxDate: "0d",
            dateFormat: "dd.mm.yy",
            altField: "#altDate",
            altFormat: "yymmdd"
        });
       // $("#datepicker").datepicker("setDate", $.datepicker.formatDate('dd.mm.yy', new Date()));

    } );

