data "terraform_remote_state" "dns" {
  backend = "s3"

  config {
    bucket = "judo-terraform-state"
    key    = "${var.environment}/${var.region}/dns/state.tfstate"
    region = "eu-west-1"
  }
}

data "terraform_remote_state" "central_dns" {
  backend = "s3"

  config {
    bucket = "judo-terraform-state"
    key    = "${var.environment}/central/dns/state.tfstate"
    region = "eu-west-1"
  }
}

data "terraform_remote_state" "elb_internal" {
  backend = "s3"

  config {
    bucket = "judo-terraform-state"
    key    = "${var.environment}/${var.region}/elb_internal/state.tfstate"
    region = "eu-west-1"
  }
}

data "terraform_remote_state" "elb_external" {
  backend = "s3"

  config {
    bucket = "judo-terraform-state"
    key    = "${var.environment}/${var.region}/elb_external/state.tfstate"
    region = "eu-west-1"
  }
}

data "terraform_remote_state" "elb_certauth" {
  backend = "s3"

  config {
    bucket = "judo-terraform-state"
    key    = "${var.environment}/${var.region}/elb_certauth/state.tfstate"
    region = "eu-west-1"
  }
}
