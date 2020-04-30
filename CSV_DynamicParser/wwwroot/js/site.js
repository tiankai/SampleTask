// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function startRead() {
    // obtain input element through DOM
    console.log('start to read files...');
    var file = document.getElementById('fileInput_CSV').files[0];
    console.log(file);
    if (file) {
        getAsText(file);
    } 
}

function getAsText(readFile) {

    var reader = new FileReader();
    // Read file into memory as UTF-8
    reader.readAsText(readFile, "UTF-8");
    // Handle progress, success, and errors
    reader.onprogress = updateProgress;
    reader.onload = loaded;
    reader.onerror = errorHandler;
}

function updateProgress(evt) {
    if (evt.lengthComputable) {
        // evt.loaded and evt.total are ProgressEvent properties
        var loaded = (evt.loaded / evt.total);
        if (loaded < 1) {
            // Increase the prog bar length
            // style.width = (loaded * 200) + "px";
            console.log('Increase the prog bar length, loaded = ' + loaded);
        }
    }
}

function loaded(evt) {
    // Obtain the read file data
    var fileString = evt.target.result;
    console.log(fileString);
    // Handle UTF-16 file dump
    if (isChinese(fileString)) {
        //Chinese Characters + Name validation
        console.log('Chinese Characters + Name validation');
    }
    else {
        // run other charset test
        console.log('run other charset test');
    }
    // xhr.send(fileString)
    console.log($('#ckInput_FileHasHeaders').prop('checked'));
    $('#CsvFile').val(fileString);
    var strHtml = '';
    $.ajax({
        url: '/home/upload',
        method: 'POST',
        data: {
            'fileContent': fileString,
            'hasHeader': $('#ckInput_FileHasHeaders').prop('checked'),
        },
        dataType: 'json'
    }).done(function (data) {
        console.log(data);
        $('#FileHeaderCount').val(data.fileHeaderCount);
        $('#divMappedTable').html('');
        if (data.errorMessage == '') {
            if (data.fileHeaderCount > 0 && data.fileHasHeaders == true) {
                $.each(data.mappedItems, function (idx, item) {
                    strHtml += getMappingTable(idx, item.order, item.csvFileHeader, data.mappingFieldList);
                });
            } else { // no headers
                var columnLens = [];
                $.each(data.dataSet, function (dataIndex, dataItem) {
                    if (Array.isArray(dataItem) == true) {
                        columnLens.push(dataItem.length);
                    }
                });
                console.log(columnLens);
                var minLen = 0;
                $.each(columnLens, function (colIndex, len) {
                    if (len > minLen) {
                        minLen = len;
                    }
                });
                console.log('min len = ' + minLen);
                // 
                for (var m = 0; m < minLen; m++) {
                    strHtml += getMappingTable(m, m, '', data.mappingFieldList);
                }
            }
        } else {
            strHtml = '<h4 style="color: #ff3d3d;">' + data.errorMessage + '</h4>';
        }        
        // 
        $('#divMappedTable').html(strHtml);

    }).fail(function (jqXHR, textStatus) {
        console.log('got error.');
        console.log(jqXHR);
        $('#divMappedTable').html('got error when sending data');
    });
}

function getMappingTable(idx, order, fieldName, mappingFieldList) {
    var strHtml = '<div class="row" style = "padding-left: 1.5rem; padding-right: 1.5rem; padding-top: 0.4rem;">';
    strHtml += '<div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 item-bottom-style">';
    strHtml += '' + order + '<input type="hidden" name="MappedItems[' + idx + '].Order" value="' + order + '" />';
    strHtml += '</div>';
    strHtml += '<div class="col-lg-4 col-md-5 col-sm-5 col-xs-5 item-bottom-style">';
    strHtml += '' + fieldName + '<input type="hidden" name="MappedItems[' + idx + '].CsvFileHeader" value="' + fieldName + '" />';
    strHtml += '</div>';
    strHtml += '<div class="col-lg-4 col-md-5 col-sm-5 col-xs-5 item-bottom-style">';
    strHtml += '<input type="hidden" id="MappedItems_' + idx + '__IsSkip" name="MappedItems[' + idx + '].IsSkip" value="true" />';
    strHtml += '<select id="MappedItems_' + idx + '__MappedField" name="MappedItems[' + idx + '].MappedField" style="min-width: 10rem;" onchange="selectForOption(' + idx + ')">';
    strHtml += '<optgroup label="Skip">';
    strHtml += '<option value="Skip">--Skip--</option>';
    strHtml += '</optgroup>';
    strHtml += '<optgroup label="Map">';
    $.each(mappingFieldList, function (index, field) {
        strHtml += '<option value="' + field.name + '">' + field.name + '</option>';
    });
    strHtml += '</optgroup>';
    strHtml += '</select>';
    strHtml += '</div>';
    strHtml += '</div>';

    return strHtml;
}

function errorHandler(evt) {
    if (evt.target.error.name == "NotReadableError") {
        // The file could not be read
        console.log('NotReadableError');
    }
}

function isChinese(fileString) {
    return false;
}

function selectForOption(itemIndex) {
    var option = $('#MappedItems_' + itemIndex + '__MappedField option:selected');
    var labelStr = option.parents('optgroup').prop('label');
    if (labelStr == 'Skip') {
        $('#MappedItems_' + itemIndex + '__IsSkip').val('true');
    } else {
        $('#MappedItems_' + itemIndex + '__IsSkip').val('false');
    }
}