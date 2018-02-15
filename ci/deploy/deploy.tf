variable "environment" {}
variable "image_tag" {}
variable "build_id" {}
variable "service_name" {}
variable "region" {}

provider "aws" {
  region = "eu-west-1"
}

module "ecs_service" {
  source                          = "git@github.com:Judopay/Terraform//modules//aws//ecs_service_internal"
  service_name                    = "${var.service_name}"
  host_headers                    = ["${var.service_name}.${var.environment}.judopay.com", "${var.service_name}.judopay.com"]
  service_port                    = "80"
  desired_count                   = "2"
  environment                     = "${var.environment}"
  task_definition                 = "${data.template_file.task_definition.rendered}"
  alb_listener_rule_priority      = "80"
  alb_stickiness_duration_seconds = "1800"
  alb_stickiness_enabled          = "true"
}

terraform {
  # The configuration for this backend will be filled in by Terragrunt
  backend "s3" {}
}

data "template_file" "task_definition" {
  template = "${file("${path.module}/taskdefinition.json")}"

  vars {
    environment  = "${var.environment}"
    image_tag    = "${var.image_tag}"
    build_id     = "${var.build_id}"
    service_name = "${var.service_name}"
  }
}

resource "aws_route53_record" "internal_additionsSDK" {
  zone_id = "${data.terraform_remote_state.dns.dns_internal_domain_zone_id}"
  name    = "${var.service_name}"
  type    = "CNAME"
  ttl     = "300"
  records = ["${data.terraform_remote_state.elb_internal.dns_name}"]
}

resource "aws_route53_record" "public_additionsSDK" {
  zone_id = "${data.terraform_remote_state.central_dns.dns_public_environment_domain_zone_id}"
  name    = "${var.service_name}"
  type    = "CNAME"
  ttl     = "300"
  records = ["${data.terraform_remote_state.elb_external.dns_name}"]
}
