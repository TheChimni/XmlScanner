$(document).ready(function () {
	$('#scanForm').submit(function (event) {
		event.preventDefault();
		$.ajax({
			url: this.action,
			type: this.method,
			dataType: 'html',
			success: function (result) { $('#scanResults').html(result); },
			data: { SelectedSampleFile: $('#SelectedSampleFile').val() }
		});
	});
});
