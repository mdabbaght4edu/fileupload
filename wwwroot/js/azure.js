
(function ($) {

	$.fn.AzureFileUpload = function (options) {
		var self = this;
		var elm = this.length > 0 ? this[0] : null;
		var options = $.extend({}, $.fn.AzureFileUpload.defaults, options);
		var isUploading = false;
		var selectedFile = null;
		var uploadOpertation = null;
		var uploadProgressIntervalHandle = null;

		if (this.length > 1) {
			this.each(function () { $(this).AzureFileUpload(options) });
			return this;
		}

		this.initialize = function () {
			$(document).on('change', elm, function (e) {
				var file = elm.files.length > 0 ? elm.files[0] : null;

				var acceptFileTypes = options.acceptFileTypes;
				if (file && !acceptFileTypes.test(file.name)) {
					_onError(options.messages.acceptFileTypes);
					elm.value = '';
				}

				var maxSize = options.maxFileSize;
				if (file && file.size > maxSize) {
					_onError(options.messages.maxFileSize);
					elm.value = '';
				}

				if (elm.value == '') {
					return false;
				}
				else {
					selectedFile = file;
					_onSelect();
					if (options && options.autoUpload === true) {
						_start();
					}
					return true;
				}
			});
			return this;
		};

		this.start = function () {
			_start();
			return this;
		}

		this.cancel = function () {
			_cancel();
			return this;
		}

		// Filter allowing us to cancel an in-progress upload by setting 'cancel' to true.
		// This doesn't cancel file chunks currently being uploaded, so the upload does continue
		// for a while after this is triggered. If the last chunks have already been sent the
		// upload might actually complete (??)
		// See http://azure.github.io/azure-storage-node/StorageServiceClient.html#withFilter
		var _cancelUploadFilter = {
			cancel: false,              // Set to true to cancel an in-progress upload

			loggingOn: false,            // Set to true to see info on console

			onCancelComplete: null,     // Set to a function you want called when a cancelled request is (probably?) complete,
			// i.e. when returnCounter==nextCounter. Because when you cancel an upload it can be many
			// seconds before the in-progress chunks complete.

			// Internal from here down

			nextCounter: 0,             // Increments each time next() is called. a.k.a. 'SentChunks' ?
			returnCounter: 0,           // Increments each time next()'s callback function is called. a.k.a. 'ReceivedChunks'?
			handle: function (requestOptions, next) {
				var self = this;
				if (self.cancel) {
					self.log('cancelling before chunk sent');
					return;
				}
				if (next) {
					self.nextCounter++;
					next(requestOptions, function (returnObject, finalCallback, nextPostCallback) {
						self.returnCounter++;
						if (self.cancel) {
							self.log('cancelling after chunk received');
							if (self.nextCounter == self.returnCounter && self.onCancelComplete) {
								self.onCancelComplete();
							}

							// REALLY ??? Is this the right way to stop the upload?
							return;
						}
						if (nextPostCallback) {
							nextPostCallback(returnObject);
						} else if (finalCallback) {
							finalCallback(returnObject);
						}
					});
				}
			},
			log: function (msg) {
				if (this.loggingOn) {
					console.log('cancelUploadFilter: ' + msg + ' nc: ' + this.nextCounter + ', rc: ' + this.returnCounter);
				}
			},
		};

		var _start = function () {
			if (!selectedFile) _onError('not selected');
			if (isUploading === true) _onError('upload process in progress.');

			// TODO: Call API to get URL, SAS Token, Container name
			var blobUri = options.blobUri;
			var containerName = options.containerName;
			var blobName = options.blobName;
			var sasToken = options.sasToken;

			AzureStorage.Blob.Constants.DEFAULT_PARALLEL_OPERATION_THREAD_COUNT = 1

			var blobService = AzureStorage.Blob.createBlobServiceWithSas(blobUri, sasToken).withFilter(_cancelUploadFilter);

			var customBlockSize = options.maxChunkSize;
			blobService.singleBlobPutThresholdInBytes = customBlockSize;
			blobService.parallelOperationThreadCount = 1;

			var uploadOptions = {
				blockSize: customBlockSize,
				parallelOperationThreadCount: 1,
				metadata: {
					isTemp: true
				}
			};

			_cancelUploadFilter.cancel = false;
			_cancelUploadFilter.onCancelComplete = function () {
				if (uploadProgressIntervalHandle) {
					clearInterval(uploadProgressIntervalHandle);
					uploadProgressIntervalHandle = null;
				}
				_onCancel();
			};

			isUploading = true;
			_onStart();
			_onProgess(0);

			var finishedOrError = false;
			uploadOpertation = blobService.createBlockBlobFromBrowserFile(containerName, blobName, selectedFile, uploadOptions, function (error, result, response) {
				finishedOrError = true;
				if (error) {
					_onError('Error in uploading process');
				} else {
					_onProgess(100);
					_onFinish();
				}
			});

			if (uploadProgressIntervalHandle) {
				clearInterval(uploadProgressIntervalHandle);
				uploadProgressIntervalHandle = null;
			}

			uploadProgressIntervalHandle = setInterval(function () {
				if (finishedOrError) {
					clearInterval(uploadProgressIntervalHandle);
					uploadProgressIntervalHandle = null;
					return;
				}
				var process = uploadOpertation.getCompletePercent();
				_onProgess(process);
			}, 200);
		}

		var _cancel = function () {
			if (!isUploading) return;
			_cancelUploadFilter.cancel = true;
		}

		var _onStart = function () {
			_log('Start');
			if (options && _isFunction(options.onStart)) options.onStart(self, selectedFile);
		}

		var _onSelect = function () {
			_log('Select');
			if (options && _isFunction(options.onSelect)) options.onSelect(self, selectedFile);
		}

		var _onError = function (error) {
			_log('Error', error);
			isUploading = false;
			if (error && options && _isFunction(options.onError)) options.onError(self, selectedFile, error);
		}

		var _onProgess = function (progress) {
			_log('Progress', progress);
			if (progress !== undefined && progress !== null && options && _isFunction(options.onProgress)) options.onProgress(self, selectedFile, progress);
		}

		var _onFinish = function () {
			_log('Finish');
			isUploading = false;
			if (options && _isFunction(options.onFinish)) options.onFinish(self, selectedFile);
		}

		var _onCancel = function () {
			_log('Cancel');
			isUploading = false;
			if (options && _isFunction(options.onCacnel)) options.onCacnel(self, file);
		}

		var _log = function (message) {
			if (!options.logging) return;
			console.log.apply(null, arguments);
		}

		var _isFunction = function (obj) { return typeof obj == 'function'; }

		return this.initialize();
	};

	$.fn.AzureFileUpload.defaults = {
		blobUri: null,
		containerName: null,
		blobName: null,
		sasToken: null,
		autoUpload: true,
		acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
		maxFileSize: 1024 * 1024 * 10,
		maxChunkSize: 1024 * 1024 * 1,
		logging: false,
		messages: {
			acceptFileTypes: 'acceptFileTypes errorr',
			maxFileSize: 'maxFileSize errorrr',
		},
		onSelect: function (azure, file) { },
		onStart: function (azure, file) { },
		onProgress: function (azure, file, progress) { },
		onCacnel: function (azure, file) { },
		onError: function (azure, file, error) { },
		onFinish: function (azure, file) { },
	};

}(jQuery));